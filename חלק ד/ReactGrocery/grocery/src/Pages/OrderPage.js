import React, { useState, useEffect } from 'react';
import axios from 'axios';

const API_BASE_URL = 'your-api-base-url'; // Replace with your actual API base URL

const OrderPage = () => {
  const [suppliers, setSuppliers] = useState([]);
  const [selectedSupplier, setSelectedSupplier] = useState('');
  const [products, setProducts] = useState([]);
  const [selectedProduct, setSelectedProduct] = useState('');
  const [order, setOrder] = useState([]);
  const [loading, setLoading] = useState(false);

  // Fetch all suppliers when component mounts
  useEffect(() => {
    fetchSuppliers();
  }, []);

  // Fetch products when a supplier is selected
  useEffect(() => {
    if (selectedSupplier) {
      fetchProductsBySupplier(selectedSupplier);
    }
  }, [selectedSupplier]);

  const fetchSuppliers = async () => {
    try {
      setLoading(true);
      const response = await axios.get(`${API_BASE_URL}/Order/orders`);
      setSuppliers(response.data);
      setLoading(false);
    } catch (error) {
      console.error('Failed to fetch suppliers:', error);
      setLoading(false);
    }
  };

  const fetchProductsBySupplier = async (supplierId) => {
    try {
      setLoading(true);
      const response = await axios.get(`${API_BASE_URL}/Supplier/${supplierId}`);
      setProducts(response.data);
      setLoading(false);
    } catch (error) {
      console.error('Failed to fetch products:', error);
      setLoading(false);
    }
  };

  const handleAddToOrder = (product) => {
    setOrder([...order, product]);
    setSelectedProduct('');
  };

  const handleFinishOrder = () => {
    // Implement order submission logic here
    console.log('Order submitted:', order);
    // Reset the form
    setOrder([]);
    setSelectedSupplier('');
    setSelectedProduct('');
  };

  return (
    <div className="max-w-4xl mx-auto p-6 bg-white rounded shadow-md">
      <h1 className="text-2xl font-bold mb-6 text-right">יצירת הזמנה</h1>
      
      {/* Supplier Selection */}
      <div className="mb-6">
        <h2 className="text-xl font-semibold mb-3 text-right">בחירת ספק</h2>
        <div className="flex justify-end">
          <select
            className="border p-2 w-full text-right"
            value={selectedSupplier}
            onChange={(e) => setSelectedSupplier(e.target.value)}
            dir="rtl"
          >
            <option value="">בחר ספק</option>
            {suppliers.map((supplier) => (
              <option key={supplier.id} value={supplier.id}>
                {supplier.name}
              </option>
            ))}
          </select>
        </div>
      </div>

      {/* Products List */}
      {selectedSupplier && (
        <div className="mb-6">
          <h2 className="text-xl font-semibold mb-3 text-right">רשימת מוצרים</h2>
          {loading ? (
            <p className="text-right">טוען מוצרים...</p>
          ) : (
            <div className="border rounded overflow-hidden">
              <table className="w-full">
                <thead>
                  <tr className="bg-gray-100">
                    <th className="p-2 text-right">פעולות</th>
                    <th className="p-2 text-right">מחיר</th>
                    <th className="p-2 text-right">שם המוצר</th>
                  </tr>
                </thead>
                <tbody>
                  {products.map((product) => (
                    <tr key={product.id} className="border-t">
                      <td className="p-2">
                        <button
                          className="bg-blue-500 text-white px-3 py-1 rounded"
                          onClick={() => handleAddToOrder(product)}
                        >
                          הוסף להזמנה
                        </button>
                      </td>
                      <td className="p-2 text-right">{product.price} ₪</td>
                      <td className="p-2 text-right">{product.name}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </div>
      )}

      {/* Order Summary */}
      {order.length > 0 && (
        <div className="mb-6">
          <h2 className="text-xl font-semibold mb-3 text-right">ההזמנה שלך</h2>
          <div className="border rounded p-4">
            <ul className="list-disc pr-5">
              {order.map((item, index) => (
                <li key={index} className="text-right mb-2">
                  {item.name} - {item.price} ₪
                </li>
              ))}
            </ul>
            <div className="mt-4 flex justify-between">
              <div className="text-xl font-bold">
                סה"כ: {order.reduce((sum, item) => sum + item.price, 0)} ₪
              </div>
            </div>
          </div>
          <div className="mt-4 flex justify-end">
            <button
              className="bg-green-500 text-white px-4 py-2 rounded"
              onClick={handleFinishOrder}
            >
              סיים הזמנה
            </button>
          </div>
        </div>
      )}
    </div>
  );
};

export default OrderPage;