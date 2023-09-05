import React, { useEffect, useState } from 'react';
import { Avatar, List, Spin, Breadcrumb } from 'antd';
import { API_URI, BASE_URI, QUERY_STRINGS } from "../config";
import { LineChartOutlined, FallOutlined, RiseOutlined } from '@ant-design/icons';
import { getApiData } from '../components/api-services/fetchHelpers';

const getAvatarIcon = (status) => {
  let defaultIcon = <LineChartOutlined style={{ color: '#FFFFFF' }} />;
  if (status % 2 === 0) {
    defaultIcon = <FallOutlined style={{ back: '#FFFFFF' }} />
  }
  else {
    defaultIcon = <RiseOutlined style={{ color: '#FFFFFF' }} />
  }
  return defaultIcon
}

const getAvatarBackground = (status) => {
  let defaultIcon = { backgroundColor: 'blue' };
  if (status % 2 === 0) {
    defaultIcon = { backgroundColor: 'blue' };
  }
  else {
    defaultIcon = { backgroundColor: 'red' };
  }
  return defaultIcon
}

var getUserList = async (setData) => {
  let url = `${BASE_URI}${API_URI.Users}`;
  let data = await getApiData(url);
  if (!!data) {
    setData(data.payload);
  }
}

export const UserList = () => {
  const [data, setData] = useState([]);

  useEffect(() => {
    let isCancelled = false;
    getUserList(setData);
    return () => {
      isCancelled = true;
    }
  }, [getUserList])
  

  return (
    <>
      {!!data && data.length > 0 &&
        <List
          pagination={{ position: 'bottom', align: 'end', pageSize: 5 }}
          dataSource={data}
          renderItem={(item, index) => (
            <List.Item>
              <List.Item.Meta
                avatar={
                  <Avatar shape="square" style={getAvatarBackground(item.userId)} icon={getAvatarIcon(item.userId)} />
                }
                title={<a href={`/details?${QUERY_STRINGS.userId}=${item.userId}`}>{item.firstName} {item.lastName}</a>}
                description={item.email}
              />
            </List.Item>
          )}
        />
      }
    </>
  )
}
