import React, { useEffect, useState } from 'react';
import { Button, Table, ConfigProvider, DatePicker } from 'antd';
import { BASE_URI, API_URI } from '../config';
import ErrorDialogBox from '../components/DialogBoxErrorLog';
import "../style/header.css";
import Axios from 'axios';
import { SearchOutlined } from '@ant-design/icons';
import moment from 'moment';

export const ErrorLogs = () => {

    const [fromDate, setFromDate] = useState(null);
    const [toDate, setToDate] = useState(new Date())
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [row, setRow] = useState(0);
    const [filteredData, setFilteredData] = useState([]);

    useEffect(() => {
        getData();
        setLoading(false);
    }, []);

    const getData = async (checked) => {
        checked = checked
        await Axios.get(BASE_URI + API_URI.ErrorLogs)
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
      };
    
      const handleToDateChange = (date, dateString) => {
        setToDate(dateString);
      };

      const validate =() => {
        if(toDate>>fromDate){
            alert('From Date must be lesser than To Date')
        }
      }

    const columns = [
        {
            title: 'Id',
            //width: 100,
            dataIndex: 'errorId',
            key: 'errorId',
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
            title: 'Error Type',
            dataIndex: 'errorType',
            key: 'errorType',
        },
        {
            title: 'Error Message',
            dataIndex: 'errorMessage',
            key: 'errorMessage',
            ellipsis: true
        },
        {
            title: 'Stack Trace',
            dataIndex: 'stackTrace',
            key: 'stackTrace',
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
            const timestamp = moment(item?.errorTimeStamp)// Assuming your data has a 'timestamp' field
            return timestamp.isBetween(moment(fromDate), moment(toDate), null, '[]');
          });
          // Update the 'data' state with the filtered data
          setFilteredData(filtered);
        }
      };

      const disabledDate = (current) => {
        // Return true to disable dates before the minimum date
        console.log(current)


        return false
      };

    return (<ConfigProvider>
        <h2 style={{ paddingRight: "1vw" }}>Error Log</h2>{" "}
        <div style={{ float: 'right', marginBottom:'1vh' }}>
            <DatePicker
                style={{ margin: '1vh' }}
                // format={"DD-MM-YY"}
                placeholder='From Date'
                onChange={handleFromDateChange}
                allowClear={false}
            />
            <DatePicker
                // format={"DD-MM-YY"}
                disabledDate={d => !d || d.isBefore(fromDate)||d.isAfter(new Date())}
                style={{ margin: '1vh' }}
                placeholder='To Date'

                
                onChange={handleToDateChange}
                allowClear={false}
            />
            <SearchOutlined style={{ margin: '1vh' }} onClick={filterData} />
        </div>
        <div className='table-container'>
            
            <Table
                columns={columns}
                dataSource={filteredData}
                loading={loading}
            />
        </div>
        <ErrorDialogBox open={open} handleClose={handleClose} data={row} />
    </ConfigProvider>)
};