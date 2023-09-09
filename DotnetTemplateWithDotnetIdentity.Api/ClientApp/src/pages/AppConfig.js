import {
    ConfigProvider,
    Table,
    Input,
    Button,
    Form,
    Modal,
    Popconfirm,
    Pagination,
    Checkbox,
    Row,
    Col
} from "antd";
import { CodeOutlined, PlusOutlined } from "@ant-design/icons";
import { BASE_URI, API_URI } from "../config";
import React, { useEffect, useState } from "react";
import Axios from "axios";
import { objectToQueryString } from "../util/utility";
import { getApiData, postData, putData } from "../components/api-services/fetchHelpers";

const { Search } = Input;

const getData = async (setAppData, isActive) => {
    let url = `${BASE_URI}${API_URI.AppConfigBase}`
    let data = await getApiData(url);
    let filteredData = data.payload.filter((item) => item.isActive === isActive).map((row) => ({
        configId: row.configId,
        configKey: row.configKey,
        configValue: row.configValue,
        isActive: row.isActive
    }));
    console.log("AppConfig Filtered Data : ", filteredData);
    setAppData(filteredData);
}

const SearchAppConfig = async (setAppData, obj) => {
    let qryStr = objectToQueryString(obj);
    console.log("qryStr : ", qryStr);
    let url = `${BASE_URI}${API_URI.AppConfigBase}${API_URI.AppConfigSearch}?${qryStr}`;
    let data = await getApiData(url);

    if (!!data)
    {
        setAppData(
            data.payload.map((row) => ({
                configId: row.configId,
                configKey: row.configKey,
                configValue: row.configValue,
                isActive: row.isActive,
            }))
        );
    }
};

const updateOrDeleteAppConfig = async (url, appData) => {
    let response = await putData(url, appData);
    console.log("updateOrDeleteAppConfig response : ", response);
};

const addConfigItem = async (url, configKey, configValue) => {
    let data = {
        configKey: configKey,
        configValue: configValue,
    };
    let response = await postData(url, data);
    console.log("Response Data : ", response);
};

const getConfigTableRowKey = (configItem) => {
    console.log("Row Data : ", configItem);
    return configItem.configKey;
}

export function AppConfig() {
    let objDefault = { id: 0, key: "", value: "", isActive: false };
    let appConfigBaseUrl = `${BASE_URI}${API_URI.AppConfigBase}`;
    
    const [appData, setAppData] = useState([]);
    const [selectedConifgItem, setSelectedConfigItem] = useState(objDefault);
  
    const [form] = Form.useForm();
    
    const [configKey, setconfigKey] = useState();
    const [configValue, setconfigValue] = useState();
    const [isAddModalOpen, setIsAddModalOpen] = useState(false);
    const [isEditModalOpen, setIsEditModalOpen] = useState(false);
    const [addCount, setAddCount] = useState(1);
    const [isActive, setIsActive] = useState(true);
    const [id, setId] = useState();
    const [checked, setChecked] = useState(true);
    const [searchText, setSearchText] = useState('');
    const [searchedColumn, setSearchedColumn] = useState('');

    const onChange = (e) => {
        setChecked(e.target.checked);
        setIsActive(e.target.checked);
        setAddCount(addCount + 1);
        settableColumn(e.target.checked? columns2:columns)
    };
    const showAddModal = () => {
        setIsAddModalOpen(true);
    };
    const handleAddOk = () => {
        addConfigItem(appConfigBaseUrl, configKey, configValue);
        setIsAddModalOpen(false);
        getData(setAppData);
        setconfigKey();
        setAddCount(addCount + 1);
    };
    const handleAddCancel = () => {
        setIsAddModalOpen(false);
        setconfigKey();
    };

    const showEditModal = (id,key,value,isActive) => {
        setSelectedConfigItem({ id: id, key: key, value: value, isActive: isActive });
        setIsEditModalOpen(true);
        setconfigKey(objDefault);
        
    };
    const handleEditOk = () => {
        let editObj = {
            configKey: selectedConifgItem.key,
            configValue: selectedConifgItem.value,
            configId: selectedConifgItem.id,
            isActive: selectedConifgItem.isActive,
        }
        updateOrDeleteAppConfig(appConfigBaseUrl, editObj);


        setIsEditModalOpen(false);
        setAddCount(addCount + 1);
        setconfigKey(objDefault);
        form.resetFields(); 
    };
    const handleEditCancel = () => {
        setIsEditModalOpen(false);
        setSelectedConfigItem(objDefault);
        form.resetFields(); 
    };

    useEffect(() => {
        getData(setAppData, isActive);
    }, [getData]);

    const columns = [
        {
            title: "Id",
            dataIndex: "configId",
            width: 75,
        },
        {
            title: "Key",
            dataIndex: "configKey",
            width: 75,
        },
        {
            title: "Value",
            dataIndex: "configValue",
            width: 90,
        },
       {
            title: "Actions",
            dataIndex: "Actions",
            width: 50,
            
            render: (_, row) => (
                <>
                    <a
                        onClick={() => {
                            setId(row.configId);
                            showEditModal(row.configId, row.configKey, row.configValue, row.isActive);
                           
                        }}
                    >
                        Edit &nbsp;
                    </a>
                </>
            )
        }
    ];

    const columns2 = [
        {
            title: "Id",
            dataIndex: "configId",
            width: 75,
        },
        {
            title: "Key",
            dataIndex: "configKey",
            width: 75,
            
        },
        {
            title: "Value",
            dataIndex: "configValue",
            width: 90,
        },
        {
            title: "Actions",
            dataIndex: "Actions",
            width: 50,
            
            render: (_, row) => (
                <>
                    <a
                        onClick={() => {
                            setId(row.configId);
                            showEditModal(row.configId, row.configKey, row.configValue, row.isActive);
                           
                        }}
                    >
                        Edit &nbsp;
                    </a>
                    <Popconfirm
                        title="Sure to delete?"
                        onConfirm={() => {
                            row.isActive = false;
                            updateOrDeleteAppConfig(appConfigBaseUrl, row);
                            setAddCount(addCount + 1);
                        }}
                    >
                        <a>Delete</a>
                    </Popconfirm>
                </>
            )
        }
       
    ];

    const [tablecolumn, settableColumn] = useState(columns);
    
    const handleSearch = (searchTem) => {
        let obj = { SearchString: searchTem, IsActive: isActive };
        SearchAppConfig(setAppData, obj);
        //setSearchText(e.toLowerCase);
        //setSearchedColumn(dataIndex);
      };
    
      // Reset search
      const handleReset = (clearFilters) => {
        clearFilters();
        setSearchText('');
      };

    return (
        <>
            <div
                style={{
                    display: "inline-block",
                    width: "100%",
                    paddingRight: "0vw",
                    marginRight: "0vw",
                }}
            >
                <div style={{ display: "inline-flex", width: "50%", top: "50%", marginBottom: '1vh' }}>
                    <h2 style={{ paddingRight: "1vw" }}>App Config</h2>{" "}
                        <PlusOutlined onClick={showAddModal}/>
                    <Modal
                        title="Add a new Configuration"
                        open={isAddModalOpen}
                        onOk={handleAddOk}
                        onCancel={handleAddCancel}
                    >
                        <Form
                            onValuesChange={(e, e1) => {
                                setconfigKey(e1.configKey);
                                setconfigValue(e1.configValue);
                            }}
                            name="wrap"
                            labelCol={{
                                flex: "110px",
                            }}
                            labelAlign="left"
                            labelWrap
                            wrapperCol={{
                                flex: 1,
                            }}
                            colon={false}
                            style={{
                                maxWidth: 600,
                            }}
                        >
                            <Form.Item
                                label="Enter Config Key"
                                name="configKey"
                                rules={[
                                    {
                                        required: true,
                                    },
                                ]}
                            >
                                <Input />
                            </Form.Item>

                            <Form.Item
                                label="Enter Config value"
                                name="configValue"
                                rules={[
                                    {
                                        required: true,
                                    },
                                ]}
                            >
                                <Input />
                            </Form.Item>
                        </Form>
                    </Modal>
                </div>

                <div style={{ display: "inline-block", marginRight: 0, width: "50%" }}>
                    <Search
                        placeholder="input search text"
                        allowClear
                        onSearch={handleSearch}
                        style={{
                            width: "15vw",
                            marginTop: "2vh",
                            float: "right",
                            paddingRight: "15",
                        }}
                    />

                    <Checkbox
                        onChange={onChange}
                        defaultChecked="true"
                        style={{ marginTop: "3vh" }}
                    >
                        Is Active
                    </Checkbox>
                </div>
            </div>
            <Modal
                title="Edit Configuration"
                open={isEditModalOpen}
                onOk={handleEditOk}
                onCancel={handleEditCancel}

            >
                <Row>
                   <Col span={5 }>
                   Enter a Key
                   </Col>
                   <Col span={5}>
                       <Input placeholder="config key" value={selectedConifgItem.key} onChange={(e) => {
                         var tempSelectedConfigItem = JSON.parse(JSON.stringify(selectedConifgItem));
                            tempSelectedConfigItem.key = e.target.value;
                            setSelectedConfigItem(tempSelectedConfigItem);
                        }} />
                  </Col>
                </Row> 
                <Row style={{marginTop:'1vh'}}>
                   <Col span={5 }>
                   Enter the Value
                   </Col>
                   <Col span={5}>
                       <Input placeholder="config key" value={selectedConifgItem.value} onChange={(e) => {
                         var tempSelectedConfigItem = JSON.parse(JSON.stringify(selectedConifgItem));
                         tempSelectedConfigItem.value = e.target.value;
                        setSelectedConfigItem(tempSelectedConfigItem);
                        }} />
                  </Col>
                </Row> 
                <Row style={{marginTop:'1vh'}}>
                   <Col span={5 }>
                   <Checkbox
                        checked={selectedConifgItem.isActive}
                        onChange={(e) => { var tempSelectedConfigItem = JSON.parse(JSON.stringify(selectedConifgItem));
                            tempSelectedConfigItem.isActive = e.target.checked;
                           setSelectedConfigItem(tempSelectedConfigItem);
                        }}
                           
                        style={{ marginTop: "3vh" }}
                    >
                        Is Active
                    </Checkbox>
                  </Col>
                </Row> 
            </Modal>

            <Table columns={tablecolumn} dataSource={appData} pagination rowKey={getConfigTableRowKey} />
        </>
    );
}
