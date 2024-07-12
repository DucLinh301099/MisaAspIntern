// src/api/service.js
import { apiClient } from './base.js';

const API_URL = '/Accountant/list'; // Adjust this endpoint based on your API's address

export const getServices = async () => {
  try {
    const response = await apiClient.get(API_URL);
    return response.data;
  } catch (error) {
    console.error('Failed to fetch services:', error);
    throw error;
  }
};

export const filterServices = (services, searchQuery) => {
  if (!services || !Array.isArray(services.data) || !services.data.length) {
    return [];
  }

  return services.data.filter(services => 
    services.name.toLowerCase().includes(searchQuery.toLowerCase())
  );
};



