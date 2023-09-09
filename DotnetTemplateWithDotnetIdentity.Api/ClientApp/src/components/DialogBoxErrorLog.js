import React, { useEffect } from 'react';
import { Table, Card, Space, Modal, Row, Col } from 'antd';
import '../style/modal.css'

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
    },
    {
        title: 'Slack Trace',
        dataIndex: 'stackTrace',
        key: 'stackTrace',
    }
];


const ErrorDialogBox = ({ open, handleClose, data }) => {
    useEffect(() => {
        console.log(data);
    }, [open])
    return (
        <Modal bodyStyle={{backgroundColor:'blue'}} width={'100%'} open={open} onCancel={handleClose} onOk={handleClose} direction="vertical" size={16}>
            <Card
                title="Error Log"
                style={{
                    width: '100%',
                }}
            >
                <Col gutter={[16, 16]}> {/* Set the gutter (spacing) between rows and columns */}
                    {data && columns.map((element, index) => (
                        <Row xs={24} sm={12} md={8} lg={6} key={index}>
                            <div style={{ display: "flex", width: "100%" }}>
                                <div className="key" style={{ fontWeight: 'bolder' }}>{element?.title + ': '}</div>
                                <div className="value" style={{ marginLeft: '10px' }}>{data[element?.key]}</div>
                                {console.log("data[element?.key]", data)}
                            </div>
                        </Row>
                    ))}
                </Col>
            </Card>
        </Modal>
    )
};
export default ErrorDialogBox;