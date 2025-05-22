import React, { useState } from 'react';

const OrdersListPage = () => {
  const [orders, setOrders] = useState([
    { id: 1, items: ['מוצר 1', 'מוצר 2'], status: 'הוזמן' },
    { id: 2, items: ['מוצר 3'], status: 'נמצא בהליך אישור' },
    { id: 3, items: ['מוצר 5'], status: 'נשלח' },
  ]);

  return (
    <div>
      <h2>מאגר הזמנות</h2>
      <ul>
        {orders.map((order) => (
          <li key={order.id}>
            <h3>הזמנה {order.id}</h3>
            <p>מוצרים: {order.items.join(', ')}</p>
            <p>סטטוס: {order.status}</p>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default OrdersListPage;
