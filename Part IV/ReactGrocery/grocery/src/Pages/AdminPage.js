import React from 'react';
import { Tab, Tabs, TabList, TabPanel } from 'react-tabs';
import 'react-tabs/style/react-tabs.css';
import OrderPage from './OrderPage';
import StatusPage from './StatusPage';
import OrdersListPage from './OrdersListPage';

const AdminPage = () => {
  return (
    <div className="admin-page-container">
      <h1 className="admin-title">דף מנהל</h1>

      <Tabs>
        <TabList>
          <Tab>הזמנת סחורה</Tab>
          <Tab>צפיה בסטטוס</Tab>
          <Tab>מאגר הזמנות</Tab>
        </TabList>

        <TabPanel>
          <OrderPage />
        </TabPanel>
        <TabPanel>
          <StatusPage />
        </TabPanel>
        <TabPanel>
          <OrdersListPage />
        </TabPanel>
      </Tabs>
    </div>
  );
};

export default AdminPage;