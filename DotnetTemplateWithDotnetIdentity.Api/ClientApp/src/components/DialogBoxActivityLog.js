import React, { useEffect } from 'react';
import { Table, Card, Space, Modal, Row, Col, ConfigProvider } from 'antd';

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
    },
    {
        title: 'Activity TimeStamp',
        dataIndex: 'activityTimeStamp',
        key: 'activityTimeStamp',
    }
];


const ActivityDialogBox = ({ open, handleClose, data }) => {
    useEffect(() => {
    }, [open])
    return (
        <ConfigProvider>
        <Modal open={open} onCancel={handleClose} onOk={handleClose} direction="vertical" size={16}>
            <Card
                title="Activity Log"
                style={{
                    width: '100%',
                }}
            >
                <Col gutter={[16, 16]}> {/* Set the gutter (spacing) between rows and columns */}
                    {data && columns.map((element, index) => (
                        <Row xs={24} sm={12} md={8} lg={6} key={index}>
                            <div style={{ display: "flex", width: "100%" }}>
                                <div className="key">{element?.title + ': '}</div>
                                <div className="value" style={{ marginLeft: '10px' }}>{data[element?.key]}</div>
                            </div>
                        </Row>
                    ))}
                </Col>
            </Card>
        </Modal>
        </ConfigProvider>
    )
};
export default ActivityDialogBox;