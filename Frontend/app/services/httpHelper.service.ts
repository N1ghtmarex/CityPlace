import { AxiosHeaderValue } from "axios";
import { useSession } from "next-auth/react";

export function buildQueryString(params: Record<string, any>): string {
    return Object.keys(params)
        .filter(key => params[key] != null)
        .map(key => `${encodeURIComponent(key)}=${encodeURIComponent(params[key])}`)
        .join('&');
}
