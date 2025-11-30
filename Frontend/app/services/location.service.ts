import { CreateLocationRequset } from '@/src/models/createLocationRequestModel';
import axiosInstance from '../../src/axios'
import { buildQueryString } from "./httpHelper.service";

export async function getLocations(searchQuery: string | null = null, limit: number | null = null, offset: number | null = null) : Promise<any> {
    const queryParams = {
        SearchQuery: searchQuery,
        Limit: limit,
        Offset: offset
    }

    const response = await axiosInstance.get(`/api/locations?${buildQueryString(queryParams)}`);

    return response.data;
}

export async function getFavoriteLocations(searchQuery: string | null = null, limit: number | null = null, offset: number | null = null) {
    const queryParams = {
        SearchQuery: searchQuery,
        Limit: limit,
        Offset: offset
    }

    const response = await axiosInstance.get(`/api/locations/favorite?${buildQueryString(queryParams)}`);

    return response.data;
}

export async function createLocation(requst: CreateLocationRequset) {
    const queryParams = {
        "Body.Name": requst.name,
        "Body.Description": requst.description,
        "Body.LocationType": requst.locationType,
        "Body.Latitude": requst.latitude,
        "Body.Longitude": requst.longitude
    }

    const response = await axiosInstance.post(`/api/admin/locations?${buildQueryString(queryParams)}`);

    return response.data;
}
