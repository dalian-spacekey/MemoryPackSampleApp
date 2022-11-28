import { Plugin } from '@nuxt/types';
import { initializeGateway } from '~/api';

const GatewayAccessorPlugin: Plugin = (context) => {

  const axiosForJson = context.$axios.create({
    baseURL: 'https://localhost:7287/web/',
    validateStatus(status) {
      return status < 401;
    },
    headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Access-Control-Allow-Origin': '*'
    }
  });

  const axiosForMemoryPack = context.$axios.create({
    baseURL: 'https://localhost:7287/web/',
    validateStatus(status) {
      return status < 401;
    },
    headers: {
      'Content-Type': 'application/x-memorypack',
      'Accept': 'application/x-memorypack',
      'Access-Control-Allow-Origin': '*'
    }
  });

  initializeGateway(axiosForJson, axiosForMemoryPack);
};

export default GatewayAccessorPlugin;