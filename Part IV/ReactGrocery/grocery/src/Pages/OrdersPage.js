import React, { useState, useEffect } from 'react';
import { getAllSuppliers, getMerchandiseBySupplier, createOrder } from './apiService';

const OrderPage = () => {
  const [suppliers, setSuppliers] = useState([]);
  const [selectedSupplierId, setSelectedSupplierId] = useState('');
  const [merchandise, setMerchandise] = useState([]);
  const [selectedItems, setSelectedItems] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  // טעינת ספקים בעת טעינת הקומפוננטה
  useEffect(() => {
    const fetchSuppliers = async () => {
      try {
        setLoading(true);
        const data = await getAllSuppliers();
        setSuppliers(data);
        setLoading(false);
      } catch (err) {
        setError('שגיאה בטעינת רשימת הספקים');
        setLoading(false);
      }
    };

    fetchSuppliers();
  }, []);

  // טיפול בבחירת ספק
  const handleSupplierChange = async (e) => {
    const supplierId = e.target.value;
    
    if (!supplierId) {
      setSelectedSupplierId('');
      setMerchandise([]);
      return;
    }
    
    try {
      setLoading(true);
      setSelectedSupplierId(supplierId);
      const merchandiseData = await getMerchandiseBySupplier(supplierId);
      setMerchandise(merchandiseData);
      setLoading(false);
    } catch (err) {
      setError('שגיאה בטעינת המוצרים של הספק');
      setLoading(false);
    }
  };

  // הוספת פריט להזמנה
  const addItemToOrder = (item) => {
    const existingItemIndex = selectedItems.findIndex(i => i.merchandiseId === item.merchandiseId);
    
    if (existingItemIndex >= 0) {
      // עדכון פריט קיים
      const updatedItems = [...selectedItems];
      updatedItems[existingItemIndex] = {
        ...updatedItems[existingItemIndex],
        quantity: updatedItems[existingItemIndex].quantity + 1
      };
      setSelectedItems(updatedItems);
    } else {
      // הוספת פריט חדש
      setSelectedItems([...selectedItems, {
        merchandiseId: item.merchandiseId,
        name: item.name,
        price: item.price,
        quantity: 1
      }]);
    }
  };

  // עדכון כמות פריט
  const updateItemQuantity = (merchandiseId, quantity) => {
    if (quantity <= 0) {
      // הסרת הפריט אם הכמות היא 0 או פחות
      setSelectedItems(selectedItems.filter(item => item.merchandiseId !== merchandiseId));
    } else {
      // עדכון הכמות
      const updatedItems = selectedItems.map(item => 
        item.merchandiseId === merchandiseId ? {...item, quantity} : item
      );
      setSelectedItems(updatedItems);
    }
  };

  // הסרת פריט מההזמנה
  const removeItemFromOrder = (merchandiseId) => {
    setSelectedItems(selectedItems.filter(item => item.merchandiseId !== merchandiseId));
  };

  // שליחת ההזמנה
  const submitOrder = async () => {
    if (selectedItems.length === 0) {
      setError('יש לבחור לפחות פריט אחד להזמנה');
      return;
    }

    try {
      setLoading(true);
      const orderData = {
        supplierId: selectedSupplierId,
        status: 'נוצרה',
        items: selectedItems.map(item => ({
          merchandiseId: item.merchandiseId,
          quantity: item.quantity
        }))
      };

      await createOrder(orderData);
      
      // איפוס הטופס לאחר שליחה מוצלחת
      setSelectedSupplierId('');
      setMerchandise([]);
      setSelectedItems([]);
      setLoading(false);
      
      alert('ההזמנה נשלחה בהצלחה');
    } catch (err) {
      setError('שגיאה בשליחת ההזמנה');
      setLoading(false);
    }
  };

  return (
    <div className="order-page">
      <h2>הזמנת סחורה</h2>
      
      {error && <div className="error-message">{error}</div>}
      
      <div className="supplier-selection">
        <h3>בחירת ספק</h3>
        <select 
          value={selectedSupplierId} 
          onChange={handleSupplierChange}
          disabled={loading}
        >
          <option value="">-- בחר ספק --</option>
          {suppliers.map(supplier => (
            <option key={supplier.id} value={supplier.id}>
              {supplier.manufacturerName}
            </option>
          ))}
        </select>
      </div>

      {loading && <div className="loading">טוען...</div>}

      {selectedSupplierId && merchandise.length > 0 && (
        <div className="merchandise-section">
          <h3>רשימת מוצרים</h3>
          <table className="merchandise-table">
            <thead>
              <tr>
                <th>שם מוצר</th>
                <th>מחיר</th>
                <th>כמות מינימלית</th>
                <th>פעולות</th>
              </tr>
            </thead>
            <tbody>
              {merchandise.map(item => (
                <tr key={item.merchandiseId}>
                  <td>{item.name}</td>
                  <td>{item.price} ₪</td>
                  <td>{item.minimumQuantity}</td>
                  <td>
                    <button 
                      onClick={() => addItemToOrder(item)} 
                      disabled={loading}
                    >
                      הוסף להזמנה
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}

      {selectedItems.length > 0 && (
        <div className="order-summary">
          <h3>פריטים בהזמנה</h3>
          <table>
            <thead>
              <tr>
                <th>שם מוצר</th>
                <th>מחיר ליחידה</th>
                <th>כמות</th>
                <th>סה"כ</th>
                <th>פעולות</th>
              </tr>
            </thead>
            <tbody>
              {selectedItems.map(item => (
                <tr key={item.merchandiseId}>
                  <td>{item.name}</td>
                  <td>{item.price} ₪</td>
                  <td>
                    <input
                      type="number"
                      min="1"
                      value={item.quantity}
                      onChange={(e) => updateItemQuantity(item.merchandiseId, parseInt(e.target.value))}
                    />
                  </td>
                  <td>{item.price * item.quantity} ₪</td>
                  <td>
                    <button 
                      className="remove-btn"
                      onClick={() => removeItemFromOrder(item.merchandiseId)}
                    >
                      הסר
                    </button>
                  </td>
                </tr>
              ))}
              <tr className="total-row">
                <td colSpan="3">סה"כ</td>
                <td colSpan="2">
                  {selectedItems.reduce((sum, item) => sum + (item.price * item.quantity), 0)} ₪
                </td>
              </tr>
            </tbody>
          </table>
          
          <button 
            className="submit-order-btn"
            disabled={loading}
            onClick={submitOrder}
          >
            {loading ? 'שולח הזמנה...' : 'שלח הזמנה'}
          </button>
        </div>
      )}
    </div>
  );
};

export default OrderPage;