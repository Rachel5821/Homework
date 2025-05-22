import axios from 'axios';

const API_BASE_URL = 'http://localhost:5117/api'; 

// Login service
export const loginService = async (username, password) => {
  try {
    const response = await axios.get(`${API_BASE_URL}/User/login/${username}/${password}`);
    return response.data; 
  } catch (error) {
    console.error('Login failed:', error);
    throw error; 
  }
};
export const registerService = async (userDto) => {
  try {
    const response = await axios.post(`${API_BASE_URL}/User`, {
      id: userDto.id || 0,
      isManager: userDto.isManager || false,
      userName: userDto.userName,
      password: userDto.password,
      order_status: userDto.order_status || 'נוצרה',
      manufacturerName: userDto.manufacturerName,
      phoneNumber: userDto.phoneNumber,
      representativeName: userDto.representativeName,
      supplierMerchandises: userDto.supplierMerchandises || [
        {
          merchandiseId: 1,
          price: 10,
          minimumQuantity: 10
        }
      ]
    });

    return response.data; 
  } catch (error) {
    console.error('Registration failed:', error);
    throw error; 
  }
};

export const getAllOrders = async () => {
  try {
    const response = await axios.get(`${API_BASE_URL}/Order/orders`);
    return response.data; 
  } catch (error) {
    console.error('Get orders failed:', error);
    throw error; 
  }
};


export const supplierOrders = async (id) => {
  try {
    const response = await axios.get(`${API_BASE_URL}/Order/${id}`);
    return response.data; 
  } catch (error) {
    console.error('Error fetching orders:', error);
    throw error; 
  }
};

export const ConfirmedOrders = async (id) => {
  try {
    const response = await axios.get(`${API_BASE_URL}/Order/completed/${id}`);
    return response.data; 
  } catch (error) {
    console.error('Error fetching orders:', error);
    throw error; 
  }
};

export const NotConfirmedOrders = async (id) => {
  try {
    const response = await axios.get(`${API_BASE_URL}/Order/Notcompleted/${id}`);
    return response.data; 
  } catch (error) {
    console.error('Error fetching orders:', error);
    throw error; 
  }
};


export const confirmOrder = async (orderId) => {
  try {
    // שולחים את הבקשה לנתיב הנכון
    const response = await axios.put(`/orders/${orderId}`);

    console.log('תשובת השרת:', response.data);

    if (response.status === 200) {
      return response.data; // מחזירים את התגובה שההזמנה אושרה
    } else {
      throw new Error('התגובה לא מכילה סטטוס תקני');
    }
  } catch (error) {
    console.error('שגיאה באישור ההזמנה:', error);
    throw error;
  }
};



export const updateOrderStatus = async (id, status) => {
  try {
    const response = await axios.put(`${API_BASE_URL}/Order/updateStatus/${id}`, JSON.stringify(status), {
      headers: {
        'Content-Type': 'application/json' 
      }
    });
    return response.data;
  } catch (error) {
    console.error('Update status failed:', error);
    throw error;
  }
};

// Get all suppliers
export const getAllSuppliers = async () => {
  try {
    const response = await axios.get(`${API_BASE_URL}/api/Supplier/suppliers`)
    return response.data; 
  } catch (error) {
    console.error('Get suppliers failed:', error);
    throw error; 
  }
};

// Get merchandise by supplier ID
export const getMerchandiseBySupplier = async (supplierId) => {
  try {
    const response = await axios.get(`${API_BASE_URL}/Supplier/${supplierId}`);
    return response.data; 
  } catch (error) {
    console.error('Get supplier merchandise failed:', error);
    throw error; 
  }
};
// Get merchandise by supplier ID
export const getProductsBySupplier = async (supplierId) => {
  try {
    const response = await axios.get(`${API_BASE_URL}/Supplier/${supplierId}`);
    return response.data; 
  } catch (error) {
    console.error('Get supplier merchandise failed:', error);
    throw error; 
  }
};
// Create a new order
export const createOrder = async (orderData) => {
  try {
    const response = await axios.post(`${API_BASE_URL}/Order`, orderData, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
    return response.data;
  } catch (error) {
    console.error('Create order failed:', error);
    throw error;
  }
};



