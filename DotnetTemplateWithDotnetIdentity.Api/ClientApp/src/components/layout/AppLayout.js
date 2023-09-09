import React, { useEffect, useState } from 'react';
import { Button, Layout, Menu, theme, Typography } from 'antd';
import {
  MenuFoldOutlined,
  MenuUnfoldOutlined,
} from '@ant-design/icons';
import { USER_THEMES, BASE_URI, API_URI } from '../../config';
import { getMenu } from '../../util/utility';
import { HeaderLayout } from './HeaderLayout';
import { useNavigate } from "react-router-dom";
import authService from '../api-authorization/AuthorizeService';
import { getApiData } from '../api-services/fetchHelpers';
import { HomeOutlined } from '@ant-design/icons';

const { Sider, Footer, Content } = Layout;

const getLoginInfo = async (setAppMenu, setuserName, setLoginUser, setIsAuthn, setOpenKeys) => {
  let isAuth = await authService.isAuthenticated();
  setIsAuthn(isAuth);
  if (isAuth) {
    let user = await authService.getUser();
    let url = `${BASE_URI}${API_URI.AppMenu}`;
    let data = await getApiData(url);
    let menus = getMenu(data?.payload);
    let keys = menus.map(s => s.key);
    let homeMenu = [{ key: '/', label: 'Home', icon: <HomeOutlined /> }];
    let items = homeMenu.concat(menus);
    setAppMenu(items);
    console.log("App Menu One : ", items);

    setuserName(`${user.first_name} ${user.last_name}`);
    setLoginUser(user);
    setOpenKeys(keys);
  }
}

export const AppLayout = (props) => {
  const { IsDarkTheme, setIsDarkTheme } = props;
  const [IsAuthn, setIsAuthn] = useState(false);
  const [LoginUser, setLoginUser] = useState(null);
  const [userName, setuserName] = useState('Guest');
  const [collapsed, setCollapsed] = useState(false);
  const [AppMenu, setAppMenu] = useState([]);
  const [openKeys, setOpenKeys] = useState([]);

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
    getLoginInfo(setAppMenu, setuserName, setLoginUser, setIsAuthn, setOpenKeys);
    return () => {
      isCancelled = true;
    }
  }, [getLoginInfo]);
  
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
        <Button
          type="text"
          icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
          onClick={() => setCollapsed(!collapsed)}
          style={{
              fontSize: '16px',
              width: 64,
              height: 64,
              position:"absolute", zIndex:"2",
              margin: '1vh'
          }}
        />
          <Sider trigger={null} collapsible
            collapsed={collapsed} collapsedWidth = {0}
            theme={IsDarkTheme ? 'dark' : 'light'}
            style={{ position:"relative", zIndex:"1" }}>
            {/* <div className="logo">
                        <img src={window.location.origin + '/logo512.png'} alt="logo" />
                    </div> */}
            <Menu
            style={{marginTop: '10vh'}}
              mode="inline"
              defaultSelectedKeys={['/']}
              onClick={({ key }) => {
                navigate(key);
              }}
              //openKeys={openKeys}
              items={AppMenu}
              theme={IsDarkTheme ? 'dark' : 'light'}
              //style={{ background: colorBgContainer }}
            />
          </Sider>
          {/* <Layout style={{
            padding: '0 24px 24px',
          }}> */}
            <Content
              style={{
                margin: '50px 50px 50px 70px',
                padding: 24,
                minHeight: 'Calc(100vh - 10em)',
                background: colorBgContainer,
              }}
            >
              {props.children}
            </Content>
            {/* <Footer style={{ textAlign: 'center' }}>&copy; Developed by Padmasekhar!</Footer> */}
            <footer
                style={{
                position: "fixed",
                bottom: "0",
                width: "100vw",
                backgroundColor: colorBgContainer,
                alignContent: "space-evenly",
                paddingLeft: "46vw",
                paddingRight: "40vw",
                paddingBottom: "1vh",
                paddingTop: "1vh",
                marginTop: "1vh",
                }}
                >
          &#169; Developed by Padmasekhar!
        </footer>
          {/* </Layout> */}
        </Layout>
      </Typography>
    </Layout>
  )
}