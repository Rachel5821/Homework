import React, { useState, useEffect } from 'react';
import { getAllOrders, updateOrderStatus } from './api';

const StatusPage = () => {
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [statusFilter, setStatusFilter] = useState('all');

  // אפשרויות סטטוס בעברית
  const statusOptions = ['נוצרה', 'אושרה', 'נשלחה', 'הושלמה', 'בוטלה'];

  // טעינת הזמנות בעת טעינת הקומפוננטה
  useEffect(() => {
    const fetchOrders = async () => {
      try {
        setLoading(true);
        const data = await getAllOrders();
        setOrders(data);
        setLoading(false);
      } catch (err) {
        setError('שגיאה בטעינת ההזמנות');
        setLoading(false);
      }
    };

    fetchOrders();
  }, []);

  // טיפול בשינוי סטטוס
  const handleStatusChange = async (orderId, newStatus) => {
    try {
      setLoading(true);
      await updateOrderStatus(orderId, newStatus);
      
      // עדכון מצב מקומי
      const updatedOrders = orders.map(order => 
        order.id === orderId ? {...order, status: newStatus} : order
      );
      
      setOrders(updatedOrders);
      setLoading(false);
    } catch (err) {
      setError('שגיאה בעדכון סטטוס ההזמנה');
      setLoading(false);
    }
  };

  // סינון הזמנות לפי סטטוס
  const filteredOrders = statusFilter === 'all' 
    ? orders 
    : orders.filter(order => order.status === statusFilter);

  return (
    <div className="status-page">
      <h2>צפיה בסטטוס הזמנות</h2>
      
      {error && <div className="error-message">{error}</div>}
      
      <div className="filter-section">
        <label>סנן לפי סטטוס: </label>
        <select 
          value={statusFilter} 
          onChange={(e) => setStatusFilter(e.target.value)}
        >
          <option value="all">הכל</option>
          {statusOptions.map(status => (
            <option key={status} value={status}>{status}</option>
          ))}
        </select>
      </div>

      {loading && orders.length === 0 ? (
        <div>טוען הזמנות...</div>
      ) : filteredOrders.length === 0 ? (
        <div>לא נמצאו הזמנות</div>
      ) : (
        <table className="orders-table">
          <thead>
            <tr>
              <th>מספר הזמנה</th>
              <th>ספק</th>
              <th>תאריך יצירה</th>
              <th>סטטוס</th>
              <th>עדכון סטטוס</th>
            </tr>
          </thead>
          <tbody>
            {filteredOrders.map(order => (
              <tr key={order.id}>
                <td>{order.id}</td>
                <td>{order.supplierName}</td>
                <td>{new Date(order.creationDate).toLocaleDateString('he-IL')}</td>
                <td>{order.status}</td>
                <td>
                  <select
                    value={order.status}
                    onChange={(e) => handleStatusChange(order.id, e.target.value)}
                    disabled={loading}
                  >
                    {statusOptions.map(status => (
                      <option key={status} value={status}>{status}</option>
                    ))}
                  </select>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default StatusPage;