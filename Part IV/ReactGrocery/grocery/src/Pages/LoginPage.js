import React, { useState } from 'react';
import { Tab, Tabs, Form, Button, Alert, Table } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { loginService, registerService } from './api';


function LoginPage() {
  const [key, setKey] = useState('register');
  
  const [loginUsername, setLoginUsername] = useState('');
  const [loginPassword, setLoginPassword] = useState('');
  
  const [manufacturerName, setManufacturerName] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [representativeName, setRepresentativeName] = useState('');
  const [supplierMerchandises, setSupplierMerchandises] = useState([
    { merchandiseId: '', price: 0, minimumQuantity: 0 }
  ]);
  
  // במקום לקבל רשימה מהשרת, נשתמש ברשימה קבועה כדוגמה
  // אחר כך אפשר להחליף את זה בקריאת API אמיתית
  const mockMerchandiseList = [
    { id: '1', name: 'עגבניות' },
    { id: '2', name: 'מלפפונים' },
    { id: '3', name: 'גזר' },
    { id: '4', name: 'תפוחי אדמה' },
    { id: '5', name: 'בצל' }
  ];
  
  const [message, setMessage] = useState('');
  
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();
    setMessage('');
    
    if (loginUsername === 'Yoram' && loginPassword === '9876') {
      setMessage('התחברת כמנהל!');
      localStorage.setItem('user', JSON.stringify({ username: 'יורם', role: 'admin' }));
      navigate('/AdminPage'); // ניווט לדף המנהל
      return;
    }
  
    try {
      const userData = await loginService(loginUsername, loginPassword);
      setMessage('התחברת בהצלחה!');
      console.log('נתוני משתמש:', userData);
      localStorage.setItem('user', JSON.stringify(userData));
      navigate('/SupplierPage');
    } catch (error) {
      console.error('Login error:', error);
      setMessage('ההתחברות נכשלה. בדוק את שם המשתמש והסיסמה.');
    }
  };
  

  const handleRegister = async (e) => {
    e.preventDefault();
    setMessage('');
    
    const validMerchandise = supplierMerchandises.filter(item => 
      item.merchandiseId && item.price > 0 && item.minimumQuantity > 0
    );

    if (validMerchandise.length === 0 && supplierMerchandises.length > 0) {
      setMessage('יש למלא מידע תקין עבור לפחות סחורה אחת');
      return;
    }
    
    const userData = {
      userName: loginUsername,
      password: loginPassword,
      manufacturerName: manufacturerName,
      phoneNumber: phoneNumber,
      representativeName: representativeName,
      supplierMerchandises: validMerchandise,
    };
    
    try {
      const response = await registerService(userData);
      if (response) {
        setMessage('ההרשמה בוצעה בהצלחה!');
        setLoginUsername('');
        setLoginPassword('');
        setManufacturerName('');
        setPhoneNumber('');
        setRepresentativeName('');
        setSupplierMerchandises([{ merchandiseId: '', price: 0, minimumQuantity: 0 }]);
        setKey('login');
      }
    } catch (error) {
      console.error('Registration error:', error);
      setMessage('שגיאה בהרשמה. אנא נסה שוב.');
    }
  };

  const addMerchandiseRow = () => {
    setSupplierMerchandises([
      ...supplierMerchandises, 
      { merchandiseId: '', price: 0, minimumQuantity: 0 }
    ]);
  };

  const removeMerchandiseRow = (index) => {
    if (supplierMerchandises.length > 1) {
      const updatedMerchandise = [...supplierMerchandises];
      updatedMerchandise.splice(index, 1);
      setSupplierMerchandises(updatedMerchandise);
    }
  };

  const handleMerchandiseChange = (index, field, value) => {
    const updatedMerchandise = [...supplierMerchandises];
    updatedMerchandise[index][field] = value;
    setSupplierMerchandises(updatedMerchandise);
  };

  return (
    <div className="container mt-5" style={{ maxWidth: '700px' }}>
      <h1 className="text-center">ברוך הבא</h1>
      
      {message && <Alert variant={message.includes('בהצלחה') ? 'success' : 'danger'}>{message}</Alert>}
      
      <Tabs activeKey={key} onSelect={(k) => setKey(k)} id="login-register-tabs" className="mb-3">
        <Tab eventKey="register" title="הרשמה">
          <Form onSubmit={handleRegister}>
            <h3>פרטי משתמש</h3>
            <Form.Group className="mb-3" controlId="formUsernameRegister">
              <Form.Label>שם משתמש</Form.Label>
              <Form.Control 
                type="text" 
                placeholder="הכנס שם משתמש" 
                value={loginUsername}
                onChange={(e) => setLoginUsername(e.target.value)}
                required
              />
            </Form.Group>

            <Form.Group className="mb-3" controlId="formPasswordRegister">
              <Form.Label>סיסמה</Form.Label>
              <Form.Control 
                type="password" 
                placeholder="הכנס סיסמה" 
                value={loginPassword}
                onChange={(e) => setLoginPassword(e.target.value)}
                required
              />
            </Form.Group>

            <h3>פרטי ספק</h3>
            <Form.Group className="mb-3" controlId="formManufacturerName">
              <Form.Label>שם יצרן</Form.Label>
              <Form.Control 
                type="text" 
                placeholder="הכנס שם יצרן"
                value={manufacturerName}
                onChange={(e) => setManufacturerName(e.target.value)}
                required
              />
            </Form.Group>

            <Form.Group className="mb-3" controlId="formPhoneNumber">
              <Form.Label>מספר טלפון</Form.Label>
              <Form.Control 
                type="text" 
                placeholder="הכנס מספר טלפון"
                value={phoneNumber}
                onChange={(e) => setPhoneNumber(e.target.value)}
                required
              />
            </Form.Group>

            <Form.Group className="mb-3" controlId="formRepresentativeName">
              <Form.Label>שם נציג</Form.Label>
              <Form.Control 
                type="text" 
                placeholder="הכנס שם נציג"
                value={representativeName}
                onChange={(e) => setRepresentativeName(e.target.value)}
                required
              />
            </Form.Group>

            <h3>רשימת סחורות</h3>
            <Table striped bordered hover>
              <thead>
                <tr>
                  <th>סחורה</th>
                  <th>מחיר ליחידה</th>
                  <th>כמות מינימלית</th>
                  <th>פעולות</th>
                </tr>
              </thead>
              <tbody>
                {supplierMerchandises.map((item, index) => (
                  <tr key={index}>
                    <td>
                      <Form.Select
                        value={item.merchandiseId}
                        onChange={(e) => handleMerchandiseChange(index, 'merchandiseId', e.target.value)}
                        required
                      >
                        <option value="">בחר סחורה</option>
                        {mockMerchandiseList.map(m => (
                          <option key={m.id} value={m.id}>
                            {m.name}
                          </option>
                        ))}
                      </Form.Select>
                    </td>
                    <td>
                      <Form.Control
                        type="number"
                        min="0.01"
                        step="0.01"
                        value={item.price}
                        onChange={(e) => handleMerchandiseChange(index, 'price', parseFloat(e.target.value))}
                        required
                      />
                    </td>
                    <td>
                      <Form.Control
                        type="number"
                        min="1"
                        value={item.minimumQuantity}
                        onChange={(e) => handleMerchandiseChange(index, 'minimumQuantity', parseInt(e.target.value))}
                        required
                      />
                    </td>
                    <td>
                      <Button 
                        variant="danger" 
                        size="sm" 
                        onClick={() => removeMerchandiseRow(index)}
                        disabled={supplierMerchandises.length === 1}
                      >
                        הסר
                      </Button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </Table>
            <Button variant="secondary" onClick={addMerchandiseRow} className="mb-3">
              הוסף סחורה נוספת
            </Button>

            <div className="d-grid gap-2 mt-4">
              <Button variant="primary" type="submit">
                הרשמה
              </Button>
            </div>
          </Form>
        </Tab>
        
        {/* טאב עבור התחברות */}
        <Tab eventKey="login" title="התחברות">
          <Form onSubmit={handleLogin}>
            <Form.Group className="mb-3" controlId="formUsernameLogin">
              <Form.Label>שם משתמש</Form.Label>
              <Form.Control 
                type="text" 
                placeholder="הכנס שם משתמש" 
                value={loginUsername}
                onChange={(e) => setLoginUsername(e.target.value)}
                required
              />
            </Form.Group>

            <Form.Group className="mb-3" controlId="formPasswordLogin">
              <Form.Label>סיסמה</Form.Label>
              <Form.Control 
                type="password" 
                placeholder="הכנס סיסמה" 
                value={loginPassword}
                onChange={(e) => setLoginPassword(e.target.value)}
                required
              />
            </Form.Group>

            <div className="d-grid gap-2">
              <Button variant="primary" type="submit">
                התחברות
              </Button>
            </div>
          </Form>
        </Tab>
      </Tabs>
    </div>
  );
}

export default LoginPage;