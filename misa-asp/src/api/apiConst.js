// định nghãi các enpoint của api 
const Api = {
    bankExpense: {
        url: 'BankAccount/bank-account',
        method: 'get'
    },
    employee: {
        url: 'Account/employee',
        method: 'get'
    },
    login: {
        url: 'Account/login',
        method: 'post'
    },
    getAllUser: {
        url: '/Account/users',
        method: 'get'
    },
    register: {
        url: 'Account/register',
        method: 'post'
    },
    createEmployee: {
        url: 'Account/createEmployee',
        method: 'post'
    },

    getUserById:{
        url: 'Account/users',
        method: 'get'
    },
    updateUser:{
       url: 'Account/update',
       method: 'put'
    },
    logout: {
        url: 'Account/logout',  
        method: 'post'
    },
    deleteUserById:{
        url:'Account/users',
        method:'delete'
    },
    bank: {
         url:'BankAccount/create-bank-account',
        method:'post'
    },
    customer: {
        url:'Customer/create-customer',
        method:'post'
    },
    password: {
        url: 'Account/forgot-password',
        method:'post'  
    },
    accountant: {
        url: 'Accountant/list',
        method:'get'
    },
    payment: {
        url: 'Payment/create-payment',
        method:'post'
    },
    withdrawList:{
        url: 'Payment/withdraw-list',
        method:'get'
    },
    deletePaymentById: {
        url: 'Payment/payment',
        method:'delete'
    },
    getWithdrawById: {
        url: 'Payment/payment',
        method:'get'
        
    },


}
export default Api;