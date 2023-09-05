import ApiAuthorzationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { PAGE_PATHS, USER_ROLE } from './config';
import { ActivityLogs } from './pages/ActivityLogs';
import { AppConfig } from './pages/AppConfig';
import { ErrorLogs } from './pages/ErrorLogs';
import { Home } from './pages/Home';
import { UserDetails } from './pages/UserDetails';
import { UserList } from './pages/UserList';

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: PAGE_PATHS.AppConfig.path,
    requireAuth: true,
    requiredRoles: [USER_ROLE.Admin],
    element: <AppConfig />
  },
  {
    path: PAGE_PATHS.ActivityLogs.path,
    requireAuth: true,
    requiredRoles: [USER_ROLE.Admin, USER_ROLE.Support],
    element: <ActivityLogs />
  },
  {
    path: PAGE_PATHS.ErrorLogs.path,
    requireAuth: true,
    requiredRoles: [USER_ROLE.Admin, USER_ROLE.Support],
    element: <ErrorLogs />
  },
  {
    path: PAGE_PATHS.UserList.path,
    requireAuth: true,
    element: <UserList />
  },
  {
    path: PAGE_PATHS.UserDetails.path,
    requireAuth: true,
    element: <UserDetails />
  },
  ...ApiAuthorzationRoutes
];

export default AppRoutes;
