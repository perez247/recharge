
export interface AppToken {
    asString: string;
    isExpired: boolean;
    userId: string;
    mobileExpiry: Date;
    isConfirmed: boolean;
    nbf: string;
    exp: string;
    iat: string;
}
