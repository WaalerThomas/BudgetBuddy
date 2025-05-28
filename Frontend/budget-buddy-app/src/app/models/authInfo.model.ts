export interface IUser {
    id: string;
    email: string;
}

export interface IAuthInfo {
    payload?: IUser,
    accessToken?: string;
    refreshToken?: string;
    expiresAt?: number
}