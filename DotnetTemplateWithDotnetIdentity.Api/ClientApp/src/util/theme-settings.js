import { theme } from "antd";

export const getUserThemeSetting = (isDarkTheme) => {
    if (isDarkTheme)
        return getDarkThemeSetting();
    return getLightThemeSetting();
}

export const getDarkThemeSetting = () => {
    return {
        algorithm: theme.darkAlgorithm,
        token: {
            "fontSize": 12,
            "colorBgElevated": "#FFFFFF",
            "headerTextColor": "#FFFFFF",
            "colorBgHeader": '#1C1C1C',
        }
    }
}

export const getLightThemeSetting = () => {
    return {
        algorithm: theme.compactAlgorithm,
        token: {
            "fontSize": 14,
            "colorBgHeader": "#092e5d",
            "headerTextColor": "#FFFFFF"
        }
    }
}
