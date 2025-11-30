import axiosInstance from '../../src/axios'
import { RegisterRequest } from "@/src/models/registerRequestModel";

export async function register(requset: RegisterRequest) : Promise<any> {
    return await axiosInstance.post("/api/users/registration", {
        username: requset.username,
        email: requset.email,
        firstName: requset.firstName,
        lastName: requset.lastName,
        password: requset.password
    });
}