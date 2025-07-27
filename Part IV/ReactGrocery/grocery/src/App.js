import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import LoginPage from './Pages/LoginPage';
import SupplierPage  from './Pages/SupplierPage'; // דף הבית שלך או הדף שאליו אתה רוצה לעבור
import AdminPage  from './Pages/AdminPage'; // דף הבית שלך או הדף שאליו אתה רוצה לעבור
function App() {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/SupplierPage" element={<SupplierPage />} /> {/* דף הבית שאליו תעבור אחרי התחברות */}
        <Route path="/" element={<LoginPage />} /> {/* ברירת מחדל */}
        <Route path='/AdminPage' element={<AdminPage/>}/>
      </Routes>
    </Router>
  );
}

export default App;
