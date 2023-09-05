import React from 'react'
import { Component } from 'react'
import { Navigate } from 'react-router-dom'
import { ApplicationPaths, QueryParameterNames } from './ApiAuthorizationConstants'
import authService from './AuthorizeService'
import { USER_ROLE } from '../../config'

export default class AuthorizeRoute extends Component {
  constructor(props) {
    super(props);

    this.state = {
      ready: false,
      authenticated: false,
      isNormalUser: true,
      isSupportUser: false,
      isAdminUser: false
    };
  }

  componentDidMount() {
    this._subscription = authService.subscribe(() => this.authenticationChanged());
    this.populateAuthenticationState();
  }

  componentWillUnmount() {
    authService.unsubscribe(this._subscription);
  }

  render() {
    const { ready } = this.state;
    var link = document.createElement("a");
    link.href = this.props.path;
    const returnUrl = `${link.protocol}//${link.host}${link.pathname}${link.search}${link.hash}`;
    const redirectUrl = `${ApplicationPaths.Login}?${QueryParameterNames.ReturnUrl}=${encodeURIComponent(returnUrl)}`;
    if (!ready) {
      return <div></div>;
    } else {
      const { element, requiredRole } = this.props;
      return this.isAuthorized(requiredRole)  ? element : <Navigate replace to={redirectUrl} />;
    }
  }

  isAuthorized(requiredRole) {
   const { authenticated, isNormalUser, isSupportUser, isAdminUser } = this.state;

   if (!authenticated) return false;

    if (isNormalUser) return true;

    if (isSupportUser && requiredRole.find(role => role === USER_ROLE.Support)) return true;

    if (isAdminUser && requiredRole.find(role => role === USER_ROLE.Admin)) return true;

    return false;
 }

  async populateAuthenticationState() {
    const authenticated = await authService.isAuthenticated();
    const user = await authService.getUser();
    console.log("User from api :", user);
    this.setState({ ready: true, 
      authenticated: authenticated, 
      isNormalUser: user.is_normal_user,
      isSupportUser: user.is_support_user,
      isAdminUser: user.is_admin_user });
  }

  async authenticationChanged() {
    this.setState({ ready: false, authenticated: false });
    await this.populateAuthenticationState();
  }
}
