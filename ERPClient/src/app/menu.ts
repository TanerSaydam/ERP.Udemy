export class MenuModel{
    name: string = "";
    icon: string = "";
    url: string = "";
    isTitle: boolean = false;
    subMenus: MenuModel[] = [];
}

export const Menus: MenuModel[] = [
    {
        name: "Ana Sayfa",
        icon: "far fa-solid fa-home",
        url: "/",
        isTitle: false,
        subMenus: []
    },
    {
        name: "Ana Group",
        icon: "far fa-solid fa-trowel-bricks",
        url: "",
        isTitle: false,
        subMenus: [
            {
                name: "Müşteriler",
                icon: "far fa-solid fa-users",
                url: "/customers",
                isTitle: false,
                subMenus:[]
            },
            {
                name: "Depolar",
                icon: "far fa-solid fa-warehouse",
                url: "/depots",
                isTitle: false,
                subMenus:[]
            },
            {
                name: "Ürünler",
                icon: "far fa-solid fa-boxes-stacked",
                url: "/products",
                isTitle: false,
                subMenus:[]
            },
            {
                name: "Reçeteler",
                icon: "far fa-solid fa-boxes-packing",
                url: "/recipes",
                isTitle: false,
                subMenus:[]
            },
        ]
    },
    {
        name: "Siparişler",
        icon: "far fa-solid fa-clipboard-list",
        url: "/orders",
        isTitle: false,
        subMenus: []
    }
]