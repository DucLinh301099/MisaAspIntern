import axios from 'axios';
import Api from '../api/apiConst';

export const base = {
  apiClient: axios.create({
    baseURL: 'http://localhost:26655/api', 
    headers: {
      'Content-Type': 'application/json',
    },
    withCredentials: true, 
  }),

  async postApi(url, params, handleSuccess, handleError, handleException, isAuthen = false) {
    try {
      const config = await this.addHeaders(isAuthen);
      const response = await this.apiClient.post(url, params, config);

      

      if (response.data && response.data.IsSuccess) {
        if (handleSuccess && typeof handleSuccess === 'function') {
          handleSuccess(response.data.data);
        }
      } else {
        if (response.data && !response.data.IsSuccess) {
          alert(response.data.message);

          if (handleError && typeof handleError === 'function') {
            handleError(response.data.data);
          }
        }
      }
      return response.data;
    } catch (error) {
      alert('Có lỗi trong quá trình xử lý.');
      
      if (handleException && typeof handleException === 'function') {
        handleException();
      }
    }
  },

  async postAuthenApi(url, params, handleSuccess, handleError, handleException) {
    this.postApi(url, params, handleSuccess, handleError, handleException, true);
  },

  async getApi(url, params, handleSuccess, handleError, handleException, isAuthen = false) {
    try {
      const config = await base.addHeaders(isAuthen);
      if (params) {
        let urlParam = Object.entries(config.params)
            .map(([key, value]) => `${key}=${value}`)
            .join('&&');

          url = url
            ? `${url}?${urlParam}`
            : `${url}`;
      }
      const response = await base.apiClient['get'](url, config);
      if (response.IsSuccess) {
        if (handleSuccess && typeof handleSuccess == 'function') {
          handleSuccess(response.data.data);
        }
      }
      else {
        if (handleError && typeof handleError == 'function') {
          handleError(response.data.data);
        }
      }
      return response.data.data;
    } catch (error) {
      alert('Có lỗi trong quá trình xử lý.');
      if (handleException && typeof handleException == 'function') {
          handleException();
        }
    }
  },

  async getAuthenApi(url, params, handleSuccess, handleError, handleException) {
    this.getApi(url, params, handleSuccess, handleError, handleException, true);
  },

  async addHeaders(isAuthen = false, config = {}) {
    const token = base.getTokenFromCookie();
    const headers = {
      ...config.headers,
      'Content-Type': 'application/json',
    };

    if (isAuthen && token != null) {
      headers['Authorization'] = `Bearer ${token}`;
    }

    return { ...config, headers };
  },

  getTokenFromCookie() {
    const cookies = document.cookie.split(';');
    for (let cookie of cookies) {
      const [name, value] = cookie.trim().split('=');
      if (name === 'AuthToken') {
        return value;
      }
    }
    return null;
  },

  buildUrlRequest(config) {
    config.url = `${config.endpoint}`;
    if (config.params) {
      if (config.method.toLowerCase() === 'get') {
        let urlParam = Object.entries(config.params)
          .map(([key, value]) => `${key}=${value}`)
          .join('&&');

        config.url = urlParam
          ? `${config.endpoint}?${urlParam}`
          : `${config.endpoint}`;
      } else {
        config.body = config.params;
      }
    }
  },
};
