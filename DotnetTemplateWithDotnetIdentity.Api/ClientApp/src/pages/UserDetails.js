import React, { useEffect, useState } from 'react';
import { useSearchParams } from 'react-router-dom'
import { API_URI, BASE_URI, QUERY_STRINGS } from '../config';
import { Card, Empty, Divider, Table, Avatar, Image } from 'antd';
import { getApiData } from '../components/api-services/fetchHelpers';
import { getData } from '../util/utility';

const { Meta } = Card;

const getUserDetails = async (setUser) => {
  let url = `${BASE_URI}${API_URI.Users}`;
  let data = await getApiData(url);
  if (!!data) {
    setUser(data.payload);
  }
}

export const UserDetails = () => {
  const [searchParams] = useSearchParams();
  const [UserId, setUserId] = useState(searchParams.get(QUERY_STRINGS.userId));
  const [User, setUser] = useState(null);

  const defaultImg = `${window.location.origin + '/PsBlueLogo.png'}`;

  useEffect(() => {
    let isCancelled = false;
    let url = `${BASE_URI}${API_URI.Users}/${UserId}`;
    getData(url, setUser);
    return () => {
      isCancelled = true;
    }
  }, [getData])

  return (
    <>
      {!!User &&
        <>
          {/* <div className='Title'>User Details</div> */}
          <h2 style={{ paddingRight: "1vw" }}>User Details</h2>{" "}
          <Card
            style={{
              width: '100%',
              height: '30vh',
              marginBottom: '10vh'
            }}
          >
            <div style={{ display: 'flex' }}>
              <div>
                <Card
                  hoverable
                  style={{
                    width: 100,
                      height: 130,
                      marginRight: '10vh'
                  }}
                cover={<Image src={`${defaultImg}`} style={{ borderRadius: '5px', width: 130, height: 130 }} />}

                >

                </Card>
              </div>
              <div className='ProfileDetailContainer' style={{ marginLeft: '5vh' }}>
                <div className='ProfileDetailContainerChild'>
                  <div style={{ marginBottom: '2vh' }}>
                    <div className='ProfileTitleLabelName'>User Id</div>
                    <div className='ProfileTitleLabelValue'>{User.userId}</div>
                  </div>
                </div>

                <div style={{ marginBottom: '2vh' }}>
                  <div className='ProfileTitleLabelName'>Name</div>
                  <div className='ProfileTitleLabelValue'>{User.firstName} {User.lastName}</div>
                </div>

                <div style={{ marginBottom: '2vh' }}>
                  <div className='ProfileTitleLabelName'>Email</div>
                  <div className='ProfileTitleLabelValue'>{User.email}</div>
                </div>
              </div>
              <Divider type="vertical" style={{ height: '20vh', borderWidth: '2px', marginTop: '10px', marginLeft: '49px' }} />
              <div>
                    <p className='ProfileTitleLabelName' style={{ marginLeft:'10vh' }} >Other details</p>
              </div>
            </div>
          </Card>
          <h3 style={{ paddingRight: "1vw" }}>Summary</h3>{" "}
          <Card
            style={{
              width: '100%',
              height: '30vh',
              marginBottom: '10vh'
            }}
          >
            <p className='ProfileTitleLabelValue'>Commodo laboris veniam labore ipsum. Ad laboris fugiat qui Lorem anim cupidatat.
              Anim aliquip ad consectetur in cillum adipisicing cillum sint ea nisi.
              Excepteur magna aliqua laborum nostrud voluptate ipsum esse irure nulla
              labore adipisicing excepteur ullamco sunt. Irure in elit sit pariatur reprehenderit officia non veniam.</p>
          </Card>
        </>}

      {!User && <Empty description={"User details were not found!"} />}
    </>
  )
}
