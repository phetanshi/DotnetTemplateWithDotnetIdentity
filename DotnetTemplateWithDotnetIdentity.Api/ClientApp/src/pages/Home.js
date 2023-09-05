import React from 'react'
import { Button } from 'antd';
import { getApiData } from '../components/api-services/fetchHelpers';
import { API_URI } from '../config';

export const Home = () => {


  const fetchData = async () => {
    let url = `${API_URI.AppMenu}`;
    let data = await fetch("api/appmenu");
    console.log("Home data : ", data.json());
  }

  return (
    <div>
      <h2>Welcome to the hackthon!</h2>
    </div>
  )
}
