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
        url: '/Account/login',
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
        url: 'Account/users/',
        method: 'get'
    },
    updateUser:{
       url: 'Account/users/',
       method: 'put'
    },
    deleteUserById:{
        url:'Account/users/',
        method:'delete'
    },

}
export default Api;