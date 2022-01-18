/*
 *
 * (c) Copyright Ascensio System Limited 2010-2021
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
*/


namespace ASC.Mail.Net.SIP.Message
{
    /// <summary>
    /// SIP Option Tags. Defined in RFC 3261 27.1, defined values are in: http://www.iana.org/assignments/sip-parameters.
    /// </summary>
    /// <remarks>
    /// Option tags are used in header fields such as Require, Supported, Proxy-Require, and 
    /// Unsupported in support of SIP compatibility mechanisms for extensions (Section 19.2). 
    /// The option tag itself is a string that is associated with a particular SIP option (that is, an extension).
    /// </remarks>
    public class SIP_OptionTags
    {
        #region Members

        /// <summary>
        /// A UA adding the early-session option tag to a message indicates that it understands the 
        /// early-session content disposition. Defined in rfc 3959.
        /// </summary>
        public const string early_session = "early-session";

        /// <summary>
        /// Extension to allow subscriptions to lists of resources. Defined in rfc 4662.
        /// </summary>
        public const string eventlist = "eventlist";

        /// <summary>
        /// When used with the Supported header, this option tag indicates support for the 
        /// History Information to be captured for requests and returned in subsequent responses. 
        /// This tag is not used in a Proxy-Require or Require header field since support of 
        /// History-Info is optional. Defined in rfc 4244.
        /// </summary>
        public const string histinfo = "histinfo";

        /// <summary>
        /// Support for the SIP Join Header. Defined in rfc 3911.
        /// </summary>
        public const string join = "join";

        /// <summary>
        /// This option tag specifies a User Agent ability of accepting a REFER request without 
        /// establishing an implicit subscription (compared to the default case defined in RFC3515).
        /// Defined in rfc 3911.
        /// </summary>
        public const string norefersub = "norefersub";

        /// <summary>
        /// A SIP UA that supports the Path extension header field includes this option tag as a 
        /// header field value in a Supported header field in all requests generated by that UA. 
        /// Intermediate proxies may use the presence of this option tag in a REGISTER request to
        /// determine whether to offer Path service for for that request. If an intermediate proxy 
        /// requires that the registrar support Path for a request, then it includes this option tag 
        /// as a header field value in a Requires header field in that request. Defined in rfc 3327.
        /// </summary>
        public const string path = "path";

        /// <summary>
        /// An offerer MUST include this tag in  the Require header field if the offer contains 
        /// one or more "mandatory" strength-tags. If all the strength-tags in the description are
        /// "optional" or "none" the offerer MUST include this tag either in a Supported header field or 
        /// in a Require header field. Defined in rfc 3312.
        /// </summary>
        public const string precondition = "precondition";

        /// <summary>
        /// This option tag is used to ensure that a server understands the callee capabilities 
        /// parameters used in the request. Defined in rfc 3840.
        /// </summary>
        public const string pref = "pref";

        /// <summary>
        /// This option tag indicates support for the Privacy mechanism. When used in the 
        /// Proxy-Require header, it indicates that proxy servers do not forward the request unless they 
        /// can provide the requested privacy service. This tag is not used in the Require or 
        /// Supported headers. Proxies remove this option tag before forwarding the request if the desired 
        /// privacy function has been performed. Defined in rfc 3323.
        /// </summary>
        public const string privacy = "privacy";

        /// <summary>
        /// This option tag indicates support for the SIP Replaces header. Defined in rfc 3891.
        /// </summary>
        public const string replaces = "replaces";

        /// <summary>
        /// Indicates or requests support for the resource priority mechanism. Defined in rfc 4412.
        /// </summary>
        public const string resource_priority = "resource-priority";

        /// <summary>
        /// The option-tag sdp-anat is defined for use in the Require and Supported SIP [RFC3261] 
        /// header fields. SIP user agents that place this option-tag in a Supported header field understand 
        /// the ANAT semantics as defined in [RFC4091]. Defined in rfc 4092.
        /// </summary>
        public const string sdp_anat = "sdp-anat";

        /// <summary>
        /// This option tag indicates support for the Security Agreement mechanism. When used in the 
        /// Require, or Proxy-Require headers, it indicates that proxy servers are required to use the Security 
        /// Agreement mechanism.  When used in the Supported header, it indicates that the User Agent Client 
        /// supports the Security Agreement mechanism. When used in the Require header in the 494 (Security Agreement 
        /// Required) or 421 (Extension Required) responses, it indicates that the User Agent Client must use the 
        /// Security Agreement Mechanism. Defined in rfc 3329.
        /// </summary>
        public const string sec_agree = "sec-agree";

        /// <summary>
        /// This option tag is used to identify the target dialog header field extension.  When used in a 
        /// Require header field, it implies that the recipient needs to support the Target-Dialog header field. 
        /// When used in a Supported header field, it implies that the sender of the message supports it. 
        /// Defined in rfc 4538.
        /// </summary>
        public const string tdialog = "tdialog";

        /// <summary>
        /// This option tag is for support of the session timer extension. Inclusion in a Supported 
        /// header field in a request or response indicates that the UA is capable of performing 
        /// refreshes according to that specification.  Inclusion in a Require header in a request 
        /// means that the UAS must understand the session timer extension to process the request.  
        /// Inclusion in a Require header field in a response indicates that the UAC must look for the 
        /// Session-Expires header field in the response, and process accordingly. Defined in rfc 4028.
        /// </summary>
        public const string timer = "timer";

        /// <summary>
        /// This option tag is for reliability of provisional responses. When present in a 
        /// Supported header, it indicates that the UA can send or receive reliable provisional
        /// responses. When present in a Require header in a request it indicates that the UAS MUST 
        /// send all provisional responses reliably. When present in a Require header in a
        /// reliable provisional response, it indicates that the response is to be sent reliably.
        /// Defined in rfc 3262.
        /// </summary>
        public const string x100rel = "100rel";

        #endregion
    }
}