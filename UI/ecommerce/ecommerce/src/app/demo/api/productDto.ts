export interface ProductDto {
    id: number;
    name?: string;
    desc?: string;
    quantity: number;
    price: number;
    image?: string;
    category?: string;
    discountPercent: number;
}
