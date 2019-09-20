
export interface AppToken {
    asString: string;
    isExpired: boolean;
    userId: string;
    mobileExpiry: Date;
    isConfirmed: string;
    nbf: string;
    exp: string;
    iat: string;
}
