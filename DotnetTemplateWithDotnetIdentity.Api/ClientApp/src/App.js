import React, { useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import { AppLayout } from './components/layout/AppLayout';
import { PAGE_PATHS, USER_SETTINGS, USER_THEMES, BASE_URI, API_URI } from "./config"
import './custom.css';
import "./style/App.css";
import { getUserThemeSetting } from './util/theme-settings';
import { ConfigProvider } from 'antd';


const App = () => {
  const [IsDarkTheme, setIsDarkTheme] = useState(USER_SETTINGS.theme === USER_THEMES.Light);

  return(
    <ConfigProvider theme={getUserThemeSetting(IsDarkTheme)}>
      <AppLayout IsDarkTheme={IsDarkTheme} setIsDarkTheme={setIsDarkTheme}>
        <Routes>
          {AppRoutes.map((route, index) => {
            const { element, requiredRoles, requireAuth, ...rest } = route;
            return <Route key={index} {...rest} element={requireAuth ? <AuthorizeRoute {...rest} requiredRoles={requiredRoles} element={element} /> : element} />;
          })}
        </Routes>
      </AppLayout>
    </ConfigProvider>
  )
}

export default App;

// export default class App extends Component {
//   static displayName = App.name;
  

//   render() {
//     return (
//       <ConfigProvider theme={getUserThemeSetting()}>
//         <Layout>
//           <Routes>
//             {AppRoutes.map((route, index) => {
//               const { element, requiredRoles, requireAuth, ...rest } = route;
//               return <Route key={index} {...rest} element={requireAuth ? <AuthorizeRoute {...rest} requiredRoles={requiredRoles} element={element} /> : element} />;
//             })}
//           </Routes>
//         </Layout>
//       </ConfigProvider>
//     );
//   }
// }
