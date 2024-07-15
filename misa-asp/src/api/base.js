

import axios from 'axios';

export const base = {
  apiClient: axios.create({
    baseURL: 'https://localhost:7173/api', 
    headers: {
      'Content-Type': 'application/json',
    },
  }),

  async addHeaders(config = {}) {
    const token = base.getTokenFromCookie();
    const headers = {
      ...config.headers,
      'Content-Type': 'application/json',
    };

    if (token != null) {
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
};
