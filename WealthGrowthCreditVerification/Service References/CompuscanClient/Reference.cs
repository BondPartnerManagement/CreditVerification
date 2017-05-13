﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WealthGrowthCreditVerification.CompuscanClient {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://webServices/", ConfigurationName="CompuscanClient.NormalSearchService")]
    public interface NormalSearchService {
        
        // CODEGEN: Parameter 'TransReplyClass' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="TransReplyClass")]
        WealthGrowthCreditVerification.CompuscanClient.DoNormalEnquiryResponse DoNormalEnquiry(WealthGrowthCreditVerification.CompuscanClient.DoNormalEnquiryRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<WealthGrowthCreditVerification.CompuscanClient.DoNormalEnquiryResponse> DoNormalEnquiryAsync(WealthGrowthCreditVerification.CompuscanClient.DoNormalEnquiryRequest request);
        
        // CODEGEN: Parameter 'return' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        WealthGrowthCreditVerification.CompuscanClient.PingServerResponse PingServer(WealthGrowthCreditVerification.CompuscanClient.PingServerRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<WealthGrowthCreditVerification.CompuscanClient.PingServerResponse> PingServerAsync(WealthGrowthCreditVerification.CompuscanClient.PingServerRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://webServices/")]
    public partial class NormalEnqRequestParamsType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string pUsrnmeField;
        
        private string pPasswrdField;
        
        private string pVersionField;
        
        private string pOriginField;
        
        private string pOrigin_VersionField;
        
        private string pInput_FormatField;
        
        private string pTransactionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string pUsrnme {
            get {
                return this.pUsrnmeField;
            }
            set {
                this.pUsrnmeField = value;
                this.RaisePropertyChanged("pUsrnme");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string pPasswrd {
            get {
                return this.pPasswrdField;
            }
            set {
                this.pPasswrdField = value;
                this.RaisePropertyChanged("pPasswrd");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string pVersion {
            get {
                return this.pVersionField;
            }
            set {
                this.pVersionField = value;
                this.RaisePropertyChanged("pVersion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string pOrigin {
            get {
                return this.pOriginField;
            }
            set {
                this.pOriginField = value;
                this.RaisePropertyChanged("pOrigin");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string pOrigin_Version {
            get {
                return this.pOrigin_VersionField;
            }
            set {
                this.pOrigin_VersionField = value;
                this.RaisePropertyChanged("pOrigin_Version");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public string pInput_Format {
            get {
                return this.pInput_FormatField;
            }
            set {
                this.pInput_FormatField = value;
                this.RaisePropertyChanged("pInput_Format");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=6)]
        public string pTransaction {
            get {
                return this.pTransactionField;
            }
            set {
                this.pTransactionField = value;
                this.RaisePropertyChanged("pTransaction");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://webServices/")]
    public partial class transReplyClass : object, System.ComponentModel.INotifyPropertyChanged {
        
        private bool transactionCompletedField;
        
        private string errorCodeField;
        
        private string errorStringField;
        
        private string retDataField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public bool transactionCompleted {
            get {
                return this.transactionCompletedField;
            }
            set {
                this.transactionCompletedField = value;
                this.RaisePropertyChanged("transactionCompleted");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string errorCode {
            get {
                return this.errorCodeField;
            }
            set {
                this.errorCodeField = value;
                this.RaisePropertyChanged("errorCode");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string errorString {
            get {
                return this.errorStringField;
            }
            set {
                this.errorStringField = value;
                this.RaisePropertyChanged("errorString");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string retData {
            get {
                return this.retDataField;
            }
            set {
                this.retDataField = value;
                this.RaisePropertyChanged("retData");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DoNormalEnquiry", WrapperNamespace="http://webServices/", IsWrapped=true)]
    public partial class DoNormalEnquiryRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://webServices/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public WealthGrowthCreditVerification.CompuscanClient.NormalEnqRequestParamsType request;
        
        public DoNormalEnquiryRequest() {
        }
        
        public DoNormalEnquiryRequest(WealthGrowthCreditVerification.CompuscanClient.NormalEnqRequestParamsType request) {
            this.request = request;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DoNormalEnquiryResponse", WrapperNamespace="http://webServices/", IsWrapped=true)]
    public partial class DoNormalEnquiryResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://webServices/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public WealthGrowthCreditVerification.CompuscanClient.transReplyClass TransReplyClass;
        
        public DoNormalEnquiryResponse() {
        }
        
        public DoNormalEnquiryResponse(WealthGrowthCreditVerification.CompuscanClient.transReplyClass TransReplyClass) {
            this.TransReplyClass = TransReplyClass;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="PingServer", WrapperNamespace="http://webServices/", IsWrapped=true)]
    public partial class PingServerRequest {
        
        public PingServerRequest() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="PingServerResponse", WrapperNamespace="http://webServices/", IsWrapped=true)]
    public partial class PingServerResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://webServices/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool @return;
        
        public PingServerResponse() {
        }
        
        public PingServerResponse(bool @return) {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface NormalSearchServiceChannel : WealthGrowthCreditVerification.CompuscanClient.NormalSearchService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class NormalSearchServiceClient : System.ServiceModel.ClientBase<WealthGrowthCreditVerification.CompuscanClient.NormalSearchService>, WealthGrowthCreditVerification.CompuscanClient.NormalSearchService {
        
        public NormalSearchServiceClient() {
        }
        
        public NormalSearchServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public NormalSearchServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NormalSearchServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NormalSearchServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WealthGrowthCreditVerification.CompuscanClient.DoNormalEnquiryResponse WealthGrowthCreditVerification.CompuscanClient.NormalSearchService.DoNormalEnquiry(WealthGrowthCreditVerification.CompuscanClient.DoNormalEnquiryRequest request) {
            return base.Channel.DoNormalEnquiry(request);
        }
        
        public WealthGrowthCreditVerification.CompuscanClient.transReplyClass DoNormalEnquiry(WealthGrowthCreditVerification.CompuscanClient.NormalEnqRequestParamsType request) {
            WealthGrowthCreditVerification.CompuscanClient.DoNormalEnquiryRequest inValue = new WealthGrowthCreditVerification.CompuscanClient.DoNormalEnquiryRequest();
            inValue.request = request;
            WealthGrowthCreditVerification.CompuscanClient.DoNormalEnquiryResponse retVal = ((WealthGrowthCreditVerification.CompuscanClient.NormalSearchService)(this)).DoNormalEnquiry(inValue);
            return retVal.TransReplyClass;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WealthGrowthCreditVerification.CompuscanClient.DoNormalEnquiryResponse> WealthGrowthCreditVerification.CompuscanClient.NormalSearchService.DoNormalEnquiryAsync(WealthGrowthCreditVerification.CompuscanClient.DoNormalEnquiryRequest request) {
            return base.Channel.DoNormalEnquiryAsync(request);
        }
        
        public System.Threading.Tasks.Task<WealthGrowthCreditVerification.CompuscanClient.DoNormalEnquiryResponse> DoNormalEnquiryAsync(WealthGrowthCreditVerification.CompuscanClient.NormalEnqRequestParamsType request) {
            WealthGrowthCreditVerification.CompuscanClient.DoNormalEnquiryRequest inValue = new WealthGrowthCreditVerification.CompuscanClient.DoNormalEnquiryRequest();
            inValue.request = request;
            return ((WealthGrowthCreditVerification.CompuscanClient.NormalSearchService)(this)).DoNormalEnquiryAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WealthGrowthCreditVerification.CompuscanClient.PingServerResponse WealthGrowthCreditVerification.CompuscanClient.NormalSearchService.PingServer(WealthGrowthCreditVerification.CompuscanClient.PingServerRequest request) {
            return base.Channel.PingServer(request);
        }
        
        public bool PingServer() {
            WealthGrowthCreditVerification.CompuscanClient.PingServerRequest inValue = new WealthGrowthCreditVerification.CompuscanClient.PingServerRequest();
            WealthGrowthCreditVerification.CompuscanClient.PingServerResponse retVal = ((WealthGrowthCreditVerification.CompuscanClient.NormalSearchService)(this)).PingServer(inValue);
            return retVal.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WealthGrowthCreditVerification.CompuscanClient.PingServerResponse> WealthGrowthCreditVerification.CompuscanClient.NormalSearchService.PingServerAsync(WealthGrowthCreditVerification.CompuscanClient.PingServerRequest request) {
            return base.Channel.PingServerAsync(request);
        }
        
        public System.Threading.Tasks.Task<WealthGrowthCreditVerification.CompuscanClient.PingServerResponse> PingServerAsync() {
            WealthGrowthCreditVerification.CompuscanClient.PingServerRequest inValue = new WealthGrowthCreditVerification.CompuscanClient.PingServerRequest();
            return ((WealthGrowthCreditVerification.CompuscanClient.NormalSearchService)(this)).PingServerAsync(inValue);
        }
    }
}
