export class ProductModel{
    id: string = "";
    name: string = "";
    type: ProductTypeEnum = new ProductTypeEnum();
    typeValue: number = 1;
    quantity: number = 0;
}

export class ProductTypeEnum{
    name: string = "";
    value: number = 1;
}

export const productTypes: ProductTypeEnum[] = [
    {
        name: "Mamül",
        value: 1
    },
    {
        name: "Yarı Mamül",
        value: 2
    }
]
