import { OrderItem } from "./OrderItem";


export class Order {
    orderId?: number;
    orderDate: Date = new Date();
    orderNumber?: string=Math.random().toString(36).substring(2,5);
    items: Array<OrderItem> = new Array<OrderItem>();


    get subtotal(): number
    {
        const result = this.items.reduce((tot,val) => {
            return tot + (val.unitPrice * val.quantity);
        }, 0);
        return result;
    }

}