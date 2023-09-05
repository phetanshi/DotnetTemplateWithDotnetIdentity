import { SettingOutlined, RightSquareOutlined, ExceptionOutlined } from "@ant-design/icons";
import { getApiData } from "../components/api-services/fetchHelpers";

export const getMenuItems = (label, key, icon, children, type) => {
    return {
        key,
        icon,
        children,
        label,
        type,
    };
}

export const getMenu = (array) => {
    let items = [];
    if (!!array) {
        let data = Array.from(array);
        if (!!data && data.length > 0) {
            data.forEach((group, idx) => {
                let mItem = Array.from(group.menuItems);
                let subMenuItems = [];
                if (!!mItem && mItem.length > 0) {
                    subMenuItems = mItem.map((itm, idx) => {
                        return getMenuItems(itm.menuItem, `${itm.menuItem.toLowerCase()}`, <SettingOutlined />);
                    });
                }
                items.push(getMenuItems(group.appMenuGroupName, `${group.appMenuGroupName.toLowerCase()}_grp`, <RightSquareOutlined />, subMenuItems));
            });
        }
    }
    return items;
}

export const getData = async (url, setData) => {
    let data = await getApiData(url);
    if (!!data) {
        setData(data.payload);
    }
}