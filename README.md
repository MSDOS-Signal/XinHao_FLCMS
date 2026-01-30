# 炘灏全链路核心管理系统 (Xinhao Intelligent Manufacturing System)

> **版本**: 1.0.0 | **技术栈**: .NET 8/9 + MAUI Blazor Hybrid | **架构**: 领域驱动设计 (DDD)

**⚠️ 重要配置提醒 (Configuration Warning)**
> **请务必在运行前修改配置文件！(Please configure before running)**
> 本项目包含敏感配置项，请勿直接使用默认值。**别生搬硬套 (Don't copy blindly)**，请根据您的实际环境调整。
> *   **数据库连接**: 请修改 `appsettings.json` 中的 `ConnectionStrings`，设置您自己的 SQL Server 地址、账号和**密码**。
> *   **安全密钥**: 请替换 `JwtSettings` 中的 `SecretKey` 为强随机字符串。
> *   **环境适配**: 确保 `launchSettings.json` 中的端口未被占用，Docker 容器内的 `host.docker.internal` 能正确指向宿主机。

**炘灏全链路核心管理系统** 是一款专为高端制造业打造的企业级一体化管理平台。系统深度融合了 **.NET 8/9** 与 **MAUI Blazor Hybrid** 技术，严格遵循 **领域驱动设计 (DDD)** 思想，旨在消除信息孤岛，实现制造过程的透明化、智能化与高效化。

本系统涵盖 ERP、WMS、MES、SCADA、SRM 等 **11大核心业务模块**，打通了从原材料采购、生产制造到成品交付的全生命周期数字化管理闭环。

---

## 1. 基础开发规范 (Development Standards)

### 1.1 技术栈要求
*   **开发语言**: C#，框架基于 **.NET 8 LTS** (核心) + .NET 9 (兼容)。
*   **前端架构**: **MAUI Blazor Hybrid** (支持Windows/macOS/iOS/Android多端，一次开发多端部署)。
    *   **UI组件**: Bootstrap 5 + ECharts (高性能交互式数据可视化，实现综合看板实时渲染)。
*   **后端API**: **ASP.NET Core WebAPI** (RESTful风格)，集成 WebSocket 实现实时消息通知。
*   **数据库**: **Microsoft SQL Server**。
    *   **多租户模式**: 按系统模块做 Schema 隔离 (如 `erp.XXX`, `wms.XXX`, `mes.XXX`)。
    *   **ORM框架**: **Entity Framework Core 8** (Code First模式)，支持自动数据库迁移 + 种子数据初始化。
*   **架构思想**: 严格遵循 **领域驱动设计 (DDD)**，实现业务与技术解耦。
*   **通信协议**:
    *   **HTTP/HTTPS**: 常规接口。
    *   **WebSocket**: 实时数据推送、跨系统联动。
    *   **MQTT / Modbus / OPC UA**: SCADA 设备通信。

### 1.2 项目结构要求
| 目录名称 | 核心职责与开发要求 |
| :--- | :--- |
| **ERPWMS.Domain** | **领域层**：包含11大系统所有实体定义(Entities)、核心接口(Interfaces)、领域服务、业务规则、领域事件，无外部依赖仅引用Shared层。 |
| **ERPWMS.Infrastructure** | **基础设施层**：包含AppDbContext数据库上下文、EF Core迁移脚本、仓储(Repository)实现、设备通信适配（MQTT/Modbus/OPC UA）、第三方组件集成，引用Domain+Shared层。 |
| **ERPWMS.WebAPI** | **后端服务层**：提供RESTful API接口，处理前端请求、依赖注入配置、跨系统联动逻辑、接口权限控制、异常统一处理，监听 `http://localhost:8080`，引用Infrastructure+Application+Shared层。 |
| **ERPWMS.Client** | **客户端层(UI)**：基于MAUI Blazor Hybrid开发，包含所有页面(Pages)、公共组件、ECharts可视化逻辑、左侧导航栏、多端适配样式，实现Windows优先兼容多端，引用Shared层。 |
| **ERPWMS.Shared** | **共享层**：包含前后端共用的DTO、枚举、工具类、常量、异常类、数据校验规则，无任何项目引用，作为底层通用依赖。 |
| **ERPWMS.Application** | **应用层**：协调领域层完成11大系统业务流程、处理跨系统协同逻辑、发布领域事件，引用Domain+Shared层，作为WebAPI与Domain的中间层。 |

---

## 2. 核心产品与UI要求 (Product & UI)

### 2.1 核心产品要求
1.  **系统定位**：高端制造业全生命周期数字化管理平台，消除11大系统信息孤岛，实现数据全链路互通。
2.  **多端适配**：MAUI Blazor Hybrid客户端优先适配Windows（PC/工业平板），兼容Android（PDA/手持终端），页面自适应。
3.  **可视化能力**：实现**综合看板**核心功能，通过ECharts展示全厂库存、订单进度、生产状态、设备告警、质量数据等实时数据。
4.  **操作体验**：核心业务流程支持**条码/RFID扫码操作**（适配PDA），无人工手动录入关键数据。
5.  **权限控制**：实现模块-菜单-按钮-数据级权限控制。
6.  **跨系统联动**：11大系统实现业务自动触发联动（如ERP采购单审核→WMS自动生成入库单→QMS生成IQC质检单→MES同步物料可用量）。
7.  **拒绝CRUD**：所有系统必须实现**落地级制造业实际业务功能**，包含业务规则、流程审批、数据统计、报表分析。

### 2.2 前端UI与交互要求
1.  **整体布局**：左侧固定导航栏（按「综合看板→核心业务→扩展模块」划分），右侧内容区。
2.  **UI主题**：**深色模式强制**。全系统采用深色背景（Bootstrap `bg-dark` 或自定义深灰），文字为白色（`text-white`）或高亮色，确保工业现场暗光环境下清晰可见。
3.  **交互反馈**：关键操作（如删除、报工）必须有明确的确认提示（`confirm` 弹窗）。
4.  **多端适配**：Windows端支持键鼠操作，Android/PDA端支持触控操作，页面无横向滚动条，按钮/输入框适配小屏。

---

## 3. 11大核心系统详解 (Core Modules)

### 📸 系统截图展示 (System Screenshots)
<div align="center">
  <img src="img/17eb7b911c184eda68cd21823a0f5880.png" width="48%" />
  <img src="img/29a7fe17da5761ddb35d3c16cdb57c28.png" width="48%" />
  <img src="img/33c5599bdd212c8ceed856696c459966.png" width="48%" />
  <img src="img/3fe8d4c76ce4e219124fc69a3fe122d4.png" width="48%" />
  <img src="img/4baf13c3f53790c16d8ea9aa14612da5.png" width="48%" />
  <img src="img/4d61ca70429bc21d0d8dc4f2b9208542.png" width="48%" />
  <img src="img/4ef9fcf68f571d2014e9d00e5f59d495.png" width="48%" />
  <img src="img/5d0b90292ac46ed19cf3ba078221ba22.png" width="48%" />
  <img src="img/66acd02b34d6120ef5c16a4c56322371.png" width="48%" />
  <img src="img/68582641d6c2e24b1f468b8093a835ed.png" width="48%" />
  <img src="img/6e6824409936c6d58b0553bafa74c21c.png" width="48%" />
  <img src="img/7ce7ada3c26385da3a2868d7c5a30a2a.png" width="48%" />
  <img src="img/9d280dc5fe33569836786e6f0579f2c4.png" width="48%" />
  <img src="img/ad6e7f0be421c33cc297965a2c93010e.png" width="48%" />
  <img src="img/c2b3acc324f04d707a90a35df8a7485f.png" width="48%" />
  <img src="img/e6a923ab56a4f2fc88820c618b2c3162.png" width="48%" />
  <img src="img/fc3a683364e1f87488bf368bef4a6900.png" width="48%" />
</div>

### 1. ERP (企业资源计划) - 统筹全企业资源，实现业财一体化
**核心目标**：打通销售/采购/生产/财务/库存数据闭环，所有业务单据自动生成财务凭证，无人工录入。
*   **销售管理**: 客户档案、报价单、销售订单、销售出库单（关联WMS）、回款管理。
*   **采购管理**: 供应商档案（对接SRM）、采购订单（关联SRM报价/APS）、采购入库单（关联WMS）、付款管理。
*   **生产管理**: 主生产计划（MPS）、物料需求计划（MRP）、生产订单（关联BOM/APS）。
*   **财务管理**: 自动生成凭证、期末结账/反结账、产品成本核算、固定资产（对接EAM）、采购成本/销售毛利分析。
*   **库存管理**: 实时同步WMS库存、库存预警、跨仓库调拨、盘点单。

### 2. WMS (仓储管理系统) - 库存精细化管理，全流程条码化
**核心目标**：库位/批次/效期精细化管理，优化出入库效率，确保账实一致，所有操作通过PDA扫码完成。
*   **基础档案**: 多仓库/库区/库位、物料条码规则、托盘/料箱容器管理。
*   **入库管理**: 采购入库、生产入库、退货入库，支持库位智能推荐、扫码收货确认。
*   **出库管理**: 销售出库、生产领料，支持扫码拣货/复核/打包、对接TMS生成运单。
*   **库存管理**: 库位占用/释放、批次/效期管理（FIFO强制控制）、PDA扫码盘点、呆滞物料预警。
*   **作业优化**: 波次管理、上架/拣货策略、作业日志全记录。

### 3. MES (制造执行系统) - 车间生产全流程管控，实现生产全追溯
**核心目标**：打通生产计划到车间执行闭环，实时监控生产过程，PDA扫码报工，实现人/机/料/法/环全追溯。
*   **工单管理**: 工单创建/拆分/下发/完工确认（同步ERP/WMS）。
*   **工艺管理**: 工艺路线、工序报工（扫码记录/加工参数/耗时）、工艺变更。
*   **生产监控**: 产线稼动率、设备OEE（对接SCADA）、生产异常上报。
*   **生产追溯**: 成品→原材料正向追溯、原材料→成品反向追溯，记录每道工序的人/机/料/法/环。
*   **人员/班组管理**: 员工档案、班组排班、计件工资核算、绩效统计。

### 4. SCADA (数据采集与监视) - 设备数据实时采集，实现远程监控与异常告警
**核心目标**：对接车间PLC/CNC/传感器等工业设备，实时采集数据，实现设备状态监控、异常告警、远程基础控制。
*   **设备连接**: 支持Modbus TCP/RTU、OPC UA/DA、MQTT、西门子S7、三菱FX等主流协议。
*   **数据采集**: 实时采集设备运行状态、加工参数、产量，支持高频时序数据存储。
*   **设备监控**: 设备状态总览看板、加工参数实时曲线、产线OEE自动计算。
*   **告警管理**: 阈值告警、状态告警、延时告警，支持系统弹窗/消息推送。
*   **报表分析**: 运行时长/停机时长/故障时长统计、OEE趋势分析。

### 5. SRM (供应商关系管理) - 供应商全生命周期管理，实现采购协同
**核心目标**：优化供应商管理与采购协同，降低采购成本，对接ERP采购模块实现数据互通。
*   **供应商准入**: 注册→资质审核→样品测试→准入审批→编码生成。
*   **供应商绩效**: 按交货及时率/质量合格率/价格/服务评分，划分等级。
*   **采购协同**: 询价/报价/定标，采购订单同步，供应商确认/发货通知。
*   **对账结算**: 采购入库与送货单对账，结算单生成对接ERP应付款。
*   **供应商门户**: 供应商自助查看订单/报价/绩效/对账数据。

### 6. TMS (运输管理系统) - 物流全流程管控，实现运单追踪与成本核算
**核心目标**：优化物流调度，跟踪运输过程，核算运输成本，对接WMS出库/ERP销售实现数据联动。
*   **基础档案**: 承运商档案、车辆档案、司机档案。
*   **运单管理**: 接收WMS发货单自动生成运单、路径规划、装货提醒。
*   **运输跟踪**: 车辆GPS实时定位、运输状态跟踪、电子签收。
*   **成本管理**: 运输费用核算、异常费用审批、运费对账。
*   **回单管理**: 回单上传/审核，生成结算单对接ERP财务。

### 7. QMS (质量管理系统) - 全流程质量管控，实现质量追溯与缺陷分析
**核心目标**：覆盖来料/生产/成品全流程质检，实现质量全追溯，对接WMS/MES/ERP实现不合格品闭环处理。
*   **IQC (来料检验)**: 接收WMS入库单生成IQC单，PDA扫码检验，不合格品隔离/退货。
*   **IPQC (过程检验)**: 巡检、首件检验、工序合格率统计、异常上报。
*   **FQC (成品检验)**: 接收MES完工单生成FQC单，全检/抽样，合格入库/返工/报废。
*   **缺陷管理**: 缺陷库、缺陷记录、鱼骨图分析。
*   **质量追溯**: 依据成品条码反查所有质检记录、原材料批次。

### 8. APS (高级计划与排程) - 智能排产，实现产能平衡与交期精准预测
**核心目标**：基于产能/物料/订单交期等约束，智能生成最优生产计划，对接ERP/MES实现计划落地。
*   **基础数据**: 产能日历、设备效率、物料齐套性、订单优先级。
*   **计划排程**: 拖拽式可视化排程界面，自动排程+手动调整，生成生产计划。
*   **交期预测**: 自动计算订单交期，风险预警。
*   **产能平衡**: 瓶颈工序识别，产能调整建议，缺料预警。
*   **计划分析**: 排程方案对比，执行偏差分析。

### 9. EAM (企业资产管理) - 设备全生命周期管理，降低故障率
**核心目标**：管理设备/固定资产全生命周期，优化维保计划，对接SCADA/MES实现设备数据互通。
*   **设备台账**: 设备基本信息、配件档案、关联产线/工序。
*   **维保管理**: 预防性维保计划、维保工单执行/验收。
*   **故障管理**: 故障上报（对接SCADA）、维修记录、故障分析。
*   **备件管理**: 备件库存预警、领用/采购计划。
*   **资产核算**: 折旧计算、报废/调拨审批。

### 10. MOM (制造运营管理) - 车间运营统一管控，整合车间全维度数据
**核心目标**：整合MES/QMS/EAM/SCADA车间系统数据，实现车间级统一运营管理，提升车间效率。
*   **车间协同**: 工单进度同步、跨部门协同处理、人员调度。
*   **绩效分析**: 车间整体OEE、员工/班组绩效排名。
*   **工艺协同**: 工艺版本管理、变更通知、参数优化。
*   **资源管理**: 资源统一视图，利用率分析。
*   **运营报表**: 车间运营大屏，多维度趋势分析。

### 11. BOM (物料清单管理) - 统一产品结构管理，支撑生产/采购/仓储
**核心目标**：管理产品物料结构，实现版本/替代料管理，对接ERP/APS/MES实现BOM全流程应用。
*   **BOM结构管理**: 多层BOM、单阶/多阶展开、工程/生产/销售BOM。
*   **版本管理**: 版本号规则、变更审批、版本对比、生效时间。
*   **替代料管理**: 主辅料关联、替代条件、MRP自动推荐。
*   **工艺路线关联**: 物料消耗定额、超定额预警。
*   **BOM验证**: 生产前齐套性检查、结构完整性检查。

---

## 4. 运行指南 (How to Run)

### 📋 环境准备
*   **.NET SDK**: 8.0 或更高版本
*   **数据库**: SQL Server 2019+ (默认连接字符串指向 `host.docker.internal` 或本地)
*   **开发工具**: Visual Studio 2022 (推荐) 或 VS Code
*   **容器环境**: Docker Desktop (可选)

### 🐳 方式一：Docker 容器化部署 (推荐后端服务)
根目录下提供了 `docker-compose.yml`，可一键启动所有后端服务。

1.  修改 `docker-compose.yml` 中的数据库连接字符串（确保容器能访问宿主机 SQL Server）。
2.  在根目录执行命令：
    ```bash
    docker-compose up -d --build
    ```
3.  容器启动后：
    *   **WebAPI**: `http://localhost:8080`
    *   **WCS/SCADA/Job**: 后台运行中

### 🐧 方式二：Linux 命令行运行 (后端/Web端)
适用于 Linux 服务器部署 (CentOS/Ubuntu)。

1.  **发布项目**:
    ```bash
    dotnet publish ERPWMS.WebAPI/ERPWMS.WebAPI.csproj -c Release -o ./publish/api
    dotnet publish ERPWMS.BlazorUI/ERPWMS.BlazorUI.csproj -c Release -o ./publish/web
    ```
2.  **运行 WebAPI**:
    ```bash
    cd ./publish/api
    dotnet ERPWMS.WebAPI.dll --urls "http://0.0.0.0:8080"
    ```
3.  **运行 Web 管理端**:
    ```bash
    cd ./publish/web
    dotnet ERPWMS.BlazorUI.dll --urls "http://0.0.0.0:5000"
    ```

### 📱 方式三：安卓 (Android) 运行方法
适用于开发调试或真机部署。

**前提**: 需安装 Visual Studio 的 MAUI 工作负载和 Android SDK。

1.  **连接设备**:
    *   开启手机/PDA 的“开发者模式”和“USB 调试”。
    *   通过 USB 连接电脑。
    *   (可选) 如需使用 ADB 调试，项目内置了 `Enable_Android_USB.bat` 辅助脚本。
2.  **命令行运行**:
    ```bash
    cd ERPWMS.Client
    # 替换 <Device-ID> 为你的设备ID (通过 adb devices 查看)
    dotnet build -t:Run -f net9.0-android
    ```
3.  **Visual Studio 运行**:
    *   将 `ERPWMS.Client` 设为启动项目。
    *   在工具栏选择连接的 Android 设备。
    *   点击 `F5` 启动调试。

### 💻 方式四：Windows 桌面端运行
1.  使用 Visual Studio 打开 `ERPWMS_MES_SuperProject.sln`。
2.  选择 `ERPWMS.Client` 或 `ERPWMS.BlazorUI` 作为启动项目。
3.  选择 `Windows Machine` 运行。

### 🖥️ 方式五：Windows Server (IIS) 部署
适用于生产环境部署 (Windows Server 2016/2019/2022/2025)。

**前置条件**:
*   Windows Server 已启用 **IIS (Web Server)** 角色。
*   已安装 **.NET 8 Hosting Bundle** (ASP.NET Core Runtime)。

**自动化部署**:
项目内置了一键部署脚本，位于 `Deploy_Scripts/` 目录。
1.  **发布项目**:
    运行 `1_Publish_All.ps1`，脚本会自动编译并发布 WebAPI、BlazorUI 及后台服务到 `C:\inetpub\ERPWMS_MES` (默认路径)。
2.  **安装服务与站点**:
    以**管理员身份**运行 PowerShell，执行 `2_Install_IIS_Services.ps1`。
    *   自动检测并安装 IIS 功能（如未安装）。
    *   自动创建 IIS 应用程序池 `ERPWMS_Pool` (No Managed Code)。
    *   自动创建站点 `ERPWMS.WebAPI` (端口 8080) 和 `ERPWMS.BlazorUI` (端口 8082)。
    *   自动安装并启动 Windows Services (WCS, SCADA, Job)。
3.  **访问验证**:
    *   API Swagger: `http://localhost:8080/swagger`
    *   Web 管理端: `http://localhost:8082`

---

## 5. 部署与运维 (Deployment)

### 数据库迁移
项目使用 EF Core Code First。首次运行前请更新数据库：
```bash
dotnet ef database update --project ERPWMS.Infrastructure --startup-project ERPWMS.WebAPI
```

### 部署脚本
`Deploy_Scripts/` 目录下包含自动化部署脚本：
*   `1_Publish_All.ps1`: 一键发布所有微服务。
*   `2_Install_IIS_Services.ps1`: 自动配置 IIS 站点。

---

## 6. 开发禁忌 (Development Guidelines)
1.  **禁止仅开发基础CRUD功能**，所有系统必须实现落地级制造业业务流程和规则。
2.  **禁止修改指定的项目结构和目录名称**。
3.  **禁止变更已指定的技术栈和核心配置**（如SQL Server主机名、WebAPI端口、ORM框架）。
4.  **禁止出现11大系统信息孤岛**，必须实现跨系统业务自动联动。
5.  **禁止前端页面仅做展示**，所有功能必须实现可操作、可提交、可保存。

---

## 7. 常见问题
*   **Docker 连不上数据库**: 请检查 `docker-compose.yml` 中的 `host.docker.internal` 是否被系统支持，或改为实际局域网 IP。
*   **Android 扫码无反应**: 请检查 `AndroidManifest.xml` 中是否已申请相机权限 `<uses-permission android:name="android.permission.CAMERA" />`。
