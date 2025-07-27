import React, { useState, useEffect } from 'react';
import { Button, Tab, Nav } from 'react-bootstrap';
import { confirmOrder, NotConfirmedOrders ,ConfirmedOrders} from './api';


function SupplierPage() {
  const [orders, setOrders] = useState([]);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);

  // שליפת המידע של הספק מ-localStorage
  const supplierData = JSON.parse(localStorage.getItem('user'));
  const supplierId = supplierData ? supplierData.id : null;

  // פונקציה לשליפת כל ההזמנות
  const fetchAllOrders = async () => {
    if (!supplierId) {
      setError('אין לך ספק מזוהה');
      return;
    }

    setLoading(true);
    setError(null);

    try {
      const data = await NotConfirmedOrders(supplierId); // אתה יכול לשלב גם את ההזמנות המאושרות כאן
      setOrders(data);
    } catch (err) {
      setError('לא הצלחנו למשוך את ההזמנות');
    } finally {
      setLoading(false);
    }
  };

  // פונקציה לשליפת הזמנות מאושרות
  const fetchConfirmedOrders = async () => {
    if (!supplierId) {
      setError('אין לך ספק מזוהה');
      return;
    }

    setLoading(true);
    setError(null);

    try {
      const data = await ConfirmedOrders(supplierId);
      setOrders(data);
    } catch (err) {
      setError('לא הצלחנו למשוך את ההזמנות המאושרות');
    } finally {
      setLoading(false);
    }
  };

  // פונקציה לשליפת הזמנות לא מאושרות
  const fetchNotConfirmedOrders = async () => {
    if (!supplierId) {
      setError('אין לך ספק מזוהה');
      return;
    }

    setLoading(true);
    setError(null);

    try {
      const data = await NotConfirmedOrders(supplierId);
      setOrders(data);
    } catch (err) {
      setError('לא הצלחנו למשוך את ההזמנות הלא מאושרות');
    } finally {
      setLoading(false);
    }
  };

  // פונקציה לאישור הזמנה
  const confirmOrderHandler = async (orderId) => {
    try {
      const confirmedOrder = await confirmOrder(orderId); // קורא לפונקציה confirmOrder
      if (confirmedOrder) {
        // אם ההזמנה אושרה, נעדכן את רשימת ההזמנות
        setOrders((prevOrders) =>
          prevOrders.map((order) =>
            order.orderId === orderId ? { ...order, orderStatus: 'מאושרת' } : order
          )
        );
      }
    } catch (error) {
      console.error('Error confirming order:', error);
    }
  };
  

  useEffect(() => {
    // שליפה אוטומטית של כל ההזמנות
    fetchAllOrders();
  }, [supplierId]);

  return (
    <div>
      <h2>הזמנות ספק</h2>
      
      <Tab.Container id="tabs-example" defaultActiveKey="allOrders">
        <Nav variant="tabs">
          <Nav.Item>
            <Nav.Link eventKey="allOrders" onClick={fetchAllOrders}>הזמנות כלליות</Nav.Link>
          </Nav.Item>
          <Nav.Item>
            <Nav.Link eventKey="confirmed" onClick={fetchConfirmedOrders}>הזמנות מאושרות</Nav.Link>
          </Nav.Item>
          <Nav.Item>
            <Nav.Link eventKey="notConfirmed" onClick={fetchNotConfirmedOrders}>הזמנות לא מאושרות</Nav.Link>
          </Nav.Item>
        </Nav>

        <Tab.Content>
          <Tab.Pane eventKey="allOrders">
            {loading && <p>טוען...</p>}
            {error && <p style={{ color: 'red' }}>{error}</p>}
            {orders.length > 0 && (
              <div>
                <h3>הזמנות כלליות</h3>
                <ul>
                  {orders.map((order) => (
                    <li key={order.orderId}>
                      <p>
                        <strong>הזמנה מספר: {order.orderId}</strong> | 
                        ספק: {order.supplierId} | 
                        כמות כללית: {order.quantity} | 
                        סטטוס: {order.orderStatus || 'לא צוין'}
                      </p>
                    </li>
                  ))}
                </ul>
              </div>
            )}
          </Tab.Pane>

          <Tab.Pane eventKey="confirmed">
            {loading && <p>טוען...</p>}
            {error && <p style={{ color: 'red' }}>{error}</p>}
            {orders.length > 0 && (
              <div>
                <h3>הזמנות מאושרות</h3>
                <ul>
                  {orders.map((order) =>
                    order.orderStatus === 'מאושרת' ? (
                      <li key={order.orderId}>
                        <p>
                          <strong>הזמנה מספר: {order.orderId}</strong> | 
                          ספק: {order.supplierId} | 
                          כמות כללית: {order.quantity} | 
                          סטטוס: {order.orderStatus || 'לא צוין'}
                        </p>
                      </li>
                    ) : null
                  )}
                </ul>
              </div>
            )}
          </Tab.Pane>

          <Tab.Pane eventKey="notConfirmed">
            {loading && <p>טוען...</p>}
            {error && <p style={{ color: 'red' }}>{error}</p>}
            {orders.length > 0 && (
              <div>
                <h3>הזמנות לא מאושרות</h3>
                <ul>
                  {orders.map((order) =>
                    order.orderStatus !== 'מאושרת' ? (
                      <li key={order.orderId}>
                        <p>
                          <strong>הזמנה מספר: {order.orderId}</strong> | 
                          ספק: {order.supplierId} | 
                          כמות כללית: {order.quantity} | 
                          סטטוס: {order.orderStatus || 'לא צוין'}
                        </p>
                        {/* כפתור אישור הזמנה */}
                        <Button 
                          variant="outline-success" 
                          onClick={() => confirmOrderHandler(order.orderId)}
                          disabled={order.orderStatus === 'מאושרת'}
                        >
                          {order.orderStatus === 'מאושרת' ? 'ההזמנה מאושרת' : 'אשר הזמנה'}
                        </Button>
                      </li>
                    ) : null
                  )}
                </ul>
              </div>
            )}
          </Tab.Pane>
        </Tab.Content>
      </Tab.Container>
    </div>
  );
}

export default SupplierPage;
