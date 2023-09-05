import React, { Component, useEffect, useState } from 'react';
import { Button, Layout, Menu, theme, Card, Switch, Space, Typography } from 'antd';
import { Container } from 'reactstrap';
import { NavMenu } from '../NavMenu';
import { USER_THEMES, BASE_URI, PAGE_PATHS, API_URI } from '../../config';
import { getMenu, getMenuItems } from '../../util/utility';
import { HeaderLayout } from './HeaderLayout';
import { useNavigate } from "react-router-dom";
import authService from '../api-authorization/AuthorizeService';
import { ApplicationPaths } from '../api-authorization/ApiAuthorizationConstants';
import { getApiData } from '../api-services/fetchHelpers';

const { Header, Sider, Footer, Content } = Layout;

const getData = async (setAppMenu) => {
  const token = await authService.getAccessToken();
  let url = `${API_URI.AppMenu}`
  const response = await fetch('api/AppMenu?_=t', {
    headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
  });
  const data = await response.json();
  let menu = getMenu(data);
  setAppMenu(menu);
}

const getLoginInfo = async (setAppMenu, setuserName, setLoginUser, setIsAuthn) => {
  let isAuth = await authService.isAuthenticated();
  console.log("isAuth : ", isAuth);
  setIsAuthn(isAuth);
  if (isAuth) {
    let user = await authService.getUser();
    setuserName(`${user.first_name} ${user.last_name}`);
    setLoginUser(user);
    getData(setAppMenu);
  }
}

export const AppLayout = (props) => {
  const { IsDarkTheme, setIsDarkTheme } = props;
  const [IsAuthn, setIsAuthn] = useState(false);
  const [LoginUser, setLoginUser] = useState(null);
  const [userName, setuserName] = useState('Gust');
  const [collapsed, setCollapsed] = useState(false);
  const [AppMenu, setAppMenu] = useState([]);

  const navigate = useNavigate();
  const { 
          token:{ 
                  colorBgContainer, 
                  colorBgLayout, 
                  colorBgHeader, 
                  headerTextColor 
            } 
        } = theme.useToken();

  useEffect(() => {
    let isCancelled = false;
    getLoginInfo(setAppMenu, setuserName, setLoginUser, setIsAuthn);
    return () => {
      isCancelled = true;
    }
  }, [getMenu, authService.isAuthenticated, authService.getUser]);
    
  console.log("IsDarkTheme App Layout : ", IsDarkTheme);
  console.log("collapsed App Layout : ", collapsed);


  return (
    <Layout>
      <Typography>
        <HeaderLayout 
          IsDarkTheme={IsDarkTheme} 
          setIsDarkTheme={setIsDarkTheme} 
          collapsed={collapsed} 
          setCollapsed={setCollapsed} 
          IsAuthn={IsAuthn}
          userName={userName}
          />
        <Layout>
          <Sider trigger={null} collapsible
            collapsed={collapsed}
            theme={IsDarkTheme ? 'dark' : 'light'}
            style={{ background: colorBgContainer }}>
            {/* <div className="logo">
                        <img src={window.location.origin + '/logo512.png'} alt="logo" />
                    </div> */}
            <Menu
              mode="inline"
              defaultSelectedKeys={['1']}
              onClick={({ key }) => {
                navigate(key);
              }}
              items={AppMenu}
              theme={IsDarkTheme ? 'dark' : 'light'}
              style={{ background: colorBgContainer }}
            />
          </Sider>
          <Layout style={{
            padding: '0 24px 24px',
          }}>
            <Content
              style={{
                margin: '24px 16px',
                padding: 24,
                minHeight: 'Calc(100vh - 10em)',
                background: colorBgContainer,
              }}
            >
              {props.children}
            </Content>
            <Footer style={{ textAlign: 'center' }}>&copy; Developed by Padmasekhar!</Footer>
          </Layout>
        </Layout>
      </Typography>
    </Layout>
  )
}



// export class Layout extends Component {
//   static displayName = Layout.name;

//   render() {
//     return (
//       <div>
//         <NavMenu />
//         <Container tag="main">
//           {this.props.children}
//         </Container>
//       </div>
//     );
//   }
// }
