import axios from "axios";
import { getSession, signIn } from "next-auth/react";


const axiosInstance = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL
});

axiosInstance.interceptors.request.use(
  async request => {
    const session = await getSession();
    if (session) {
      request.headers.Authorization = `Bearer ${session.accessToken}`;
    }

    return request;
  });

axiosInstance.interceptors.response.use(
  response => {
    return response;
  },
  async error => {
    if (error.response && error.response.status == 401) {
      await signIn('keycloak');
    }

    return Promise.reject(error);
  }
);

export default axiosInstance;