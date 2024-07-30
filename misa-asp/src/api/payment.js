import { base } from '../api/base.js';
import Api from '../api/apiConst.js';

export const payment = {
  async createPayment(payment) {
    return await base.postAuthenApi(Api.payment.url, payment);
  },
};
