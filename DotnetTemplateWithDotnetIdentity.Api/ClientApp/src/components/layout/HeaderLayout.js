import React, { useEffect, useState } from 'react';
import { Button, Layout, theme, Switch, Space } from 'antd';
import { NavLink, useNavigate, Link } from "react-router-dom";
import { USER_THEMES, BASE_URI, PAGE_PATHS, API_URI } from '../../config';

import { ApplicationPaths } from '../api-authorization/ApiAuthorizationConstants';
import {
    MenuFoldOutlined,
    MenuUnfoldOutlined,
    LoginOutlined,
    PoweroffOutlined
} from '@ant-design/icons';


const { Header } = Layout;

export const HeaderLayout = (props) => {
    const { IsDarkTheme, 
                setIsDarkTheme, 
                collapsed, 
                setCollapsed,
                IsAuthn,
                userName } = props;

    const navigate = useNavigate();

    const logout = () => {
        navigate.state = { local: true };
        navigate(ApplicationPaths.LogOut);
    }

    const {
        token: {
            colorBgContainer,
            colorBgLayout,
            colorBgHeader,
            headerTextColor
        }
    } = theme.useToken();
    const logoutState = { local: true };


  return (
      <Header
          style={{
              padding: 0,
              background: colorBgHeader
          }}
          className='header-content'
      >
          <div>
              <Space>
                  <Button
                      type="text"
                      icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
                      onClick={() => setCollapsed(!collapsed)}
                      style={{
                          fontSize: '16px',
                          width: 64,
                          height: 64,
                          color: headerTextColor
                      }}
                  />
              </Space>
          </div>

          <div className="header-login-content">
              <Space>
                  <div style={{ paddingRight: '8px' }}>
                      <Switch size="small" checked={IsDarkTheme} checkedChildren={USER_THEMES.Dark}
                          unCheckedChildren={USER_THEMES.Light} onChange={(checked) => setIsDarkTheme(checked)} />
                  </div>
                  {IsAuthn && userName !== "Gust" &&
                      <>
                          <p style={{ color: headerTextColor, marginBottom: "0px" }}>Hello, {userName}!</p>
                      <NavLink replace tag={Link} to={ApplicationPaths.LogOut} state={logoutState}>
                          <Button
                              type="text"
                              icon={<PoweroffOutlined />}
                              style={{
                                  fontSize: '16px',
                                  width: 64,
                                  height: 64,
                                  color: 'red'
                              }}
                          />
                      </NavLink>
                      </>
                  }
                  {(!IsAuthn || userName === "Gust") &&
                      <>
                          <Button type="link"
                              style={{ fontSize: '16px', color: headerTextColor }}
                              onClick={() => navigate(ApplicationPaths.Register)}
                          >Register</Button>
                          <Button
                              type="text"
                              icon={<LoginOutlined />}
                              onClick={() => navigate(ApplicationPaths.Login)}
                              style={{
                                  fontSize: '16px',
                                  width: 64,
                                  height: 64,
                                  float: 'right',
                                  color: headerTextColor
                              }}
                          />
                      </>
                  }
              </Space>
          </div>
      </Header>
  )
}
