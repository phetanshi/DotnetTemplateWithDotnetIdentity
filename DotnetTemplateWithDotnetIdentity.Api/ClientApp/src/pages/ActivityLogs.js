import React, { useEffect, useState } from 'react';
import { Button, Table, ConfigProvider, DatePicker } from 'antd';
import { BASE_URI, API_URI } from '../config';
import ActivityDialogBox from '../components/DialogBoxActivityLog';
import "../style/header.css"
import Axios from 'axios';
import { SearchOutlined } from '@ant-design/icons';
import moment from 'moment';

export const ActivityLogs = () => {

    const [fromDate, setFromDate] = useState(null);
    const [toDate, setToDate] = useState(null)
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [row, setRow] = useState(null);
    const [filteredData, setFilteredData] = useState([]);

    useEffect(() => {
        getData();
        setLoading(false);
    }, []);

    const getData = async (checked) => {
        checked = checked
        await Axios.get(BASE_URI + API_URI.ActivityLogs)
            .then((res) => {
                console.log(res);
                setData(res?.data?.payload);
                setFilteredData(res?.data?.payload)
            })
            .catch((e) => console.log(e));
    };

    const [open, setOpen] = useState(false)
    const handleClose = () => {
        setOpen(false)
    }

    const handleFromDateChange = (date, dateString) => {
        setFromDate(dateString);
        console.log('fromDate', fromDate)
      };
    
      const handleToDateChange = (date, dateString) => {
        setToDate(dateString);
      };

    const columns = [
        {
            title: 'Id',
            //width: 100,
            dataIndex: 'activityId',
            key: 'activityId',
        },
        {
            title: 'Employee Id',
            //width: 100,
            dataIndex: 'employeeId',
            key: 'employeeId',
        },
        {
            title: 'Employee Name',
            dataIndex: 'employeeName',
            key: 'employeeName',
            ellipsis: true
        },
        {
            title: 'Activity Type',
            dataIndex: 'activityType',
            key: 'activityType',
        },
        {
            title: 'Activity Description',
            dataIndex: 'activityDesc',
            key: 'activityDesc',
            ellipsis: true
        },
        {
            title: 'Activity TimeStamp',
            dataIndex: 'activityTimeStamp',
            key: 'activityTimeStamp',
            ellipsis: true
        },
        {
            title: 'Action',
            key: 'operation',
            //width: 100,
            render: (text, record) => <Button type='text' style={{ textDecoration: 'underline' }} onClick={(e) => { setOpen(true); setRow(record) }}>Details</Button>,
        },
    ];

    const filterData = () => {
        if (fromDate && toDate) {
          const filtered = data?.filter(item => {
            console.log("This is the item: ", item)
            const timestamp = moment(item?.activityTimeStamp)// Assuming your data has a 'timestamp' field
            return timestamp.isBetween(moment(fromDate), moment(toDate), null, '[]');
          });
          // Update the 'data' state with the filtered data
          setFilteredData(filtered);
        }
      };
      
    return (<ConfigProvider>
        <h2 style={{ paddingRight: "1vw" }}>Activity Log</h2>{" "}
        <div style={{ float: 'right', marginBottom: '1vh' }}>
            <DatePicker
            style={{margin: '1vh'}}
            // format={"DD-MM-YY"}
            placeholder='From Date'
            onChange={handleFromDateChange}
            allowClear={false}
            />
            <DatePicker
                // format={"DD-MM-YY"}
                style={{ margin: '1vh' }}
                placeholder='To Date'
                onChange={handleToDateChange}
                allowClear={false}
            />
            <SearchOutlined style={{ margin: '1vh' }} onClick={filterData} />
        </div>
        {console.log('data2', fromDate, toDate, data)}
        <div className='table-container'>
            <Table
                columns={columns}
                dataSource={filteredData}
                loading={loading}
            />
        </div>
        <ActivityDialogBox open={open} handleClose={handleClose} data={row} />
    </ConfigProvider>)
};

