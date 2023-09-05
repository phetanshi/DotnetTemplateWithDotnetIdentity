// export const BASE_URI = "https://localhost:7263";
export const BASE_URI = "";


export const PAGE_PATHS = {
    ErrorLogs: { path: "/errorlogs", label: "Error Logs" },
    ActivityLogs: { path: "/activitylogs", label: "Activity Logs" },
    UserDetails: { path: "/details", label: "User Details" },
    AppConfig: { path: "/appconfig", label: "App Config" },
    UserList: { path: "/", label: "User List" },
    UserListTwo: { path: "/user", label: "User List" }
};

export const API_URI = {
    Employee: '/api/User',
    ActivityLogs: '/api/Logger',
    ErrorLogs: '/api/Logger/error',
    AppMenu: '/api/AppMenu'
}

export const USER_ROLE = {
    Admin: 'admin',
    Support: 'Support',
    Normal: 'normal'
}

export const USER_THEMES = {
    Dark: "Dark",
    Light: "Light"
}

export const QUERY_STRINGS = {
    userId: "userId"
}

export const USER_SETTINGS = {
    theme: USER_THEMES.Dark
}

export const MESSAGES = {
    API_CALL_ERROR: "Something went wrong in the API call."
}