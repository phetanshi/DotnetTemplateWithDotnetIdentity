export const getMenuItems = (label, key, icon, children, type) => {
    return {
        key,
        icon,
        children,
        label,
        type,
    };
}

export const getMenu = (response) => {
    let items = [];
    if (!!response) {
        let data = Array.from(response?.payload);
        if (!!data && data.length > 0) {
            data.forEach((group, idx) => {
                let mItem = Array.from(group.menuItems);
                let sumMenuItems = [];
                if (!!mItem && mItem.length > 0) {
                    sumMenuItems = mItem.map((itm, idx) => {
                        return getMenuItems(itm.menuItem, itm.menuItem.toLowerCase());
                    });
                }
                items.push(getMenuItems(group.appMenuGroupName, group.appMenuGroupName.toLowerCase(), null, sumMenuItems));
            });
        }
    }
    return items;
}