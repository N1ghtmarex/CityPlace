import { DefaultSession, Profile } from "next-auth";

declare module "next-auth"{
    interface Session{
        user: DefaultSession["user"];
        accessToken?: string | unknown;
        roles: string[] | any;
    }
}