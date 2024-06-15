export interface Payment {
    id?: number;
    userId?: number;
    paymentType?: string;
    provider?: string;
    accountNo?: number;
    expiry?: string;
}
