import React from 'react'
import { Button } from 'antd';
import { getApiData } from '../components/api-services/fetchHelpers';
import { API_URI } from '../config';

export const Home = () => {


  const fetchData = async () => {
    let url = `${API_URI.AppMenu}`;
    let data = await fetch("api/Logger");
    console.log("Home data : ", data.json());
  }

  return (
    <div>
      <Button type="primary" size={"large"} onClick={fetchData}>
        Fetch Menu
      </Button>
    </div>
  )
}
