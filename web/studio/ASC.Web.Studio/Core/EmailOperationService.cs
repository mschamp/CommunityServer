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


using System;
using System.Web;

using AjaxPro;

using ASC.Core;
using ASC.Core.Users;
using ASC.MessagingSystem;
using ASC.Web.Studio.Core.Notify;
using ASC.Web.Studio.Core.Users;
using ASC.Web.Studio.PublicResources;

namespace ASC.Web.Studio.Core
{
    [AjaxNamespace("EmailOperationService")]
    public class EmailOperationService
    {
        public class InvalidEmailException : Exception
        {
            public InvalidEmailException()
            {
            }

            public InvalidEmailException(string message) : base(message)
            {
            }
        }

        public class AccessDeniedException : Exception
        {
            public AccessDeniedException()
            {
            }

            public AccessDeniedException(string message) : base(message)
            {
            }
        }

        public class UserNotFoundException : Exception
        {
            public UserNotFoundException()
            {
            }

            public UserNotFoundException(string message) : base(message)
            {
            }
        }

        public class InputException : Exception
        {
            public InputException()
            {
            }

            public InputException(string message) : base(message)
            {
            }
        }

        /// <summary>
        /// Sends the email activation instructions to the specified email
        /// </summary>
        /// <param name="userID">The ID of the user who should activate the email</param>
        /// <param name="email">Email</param>
        [AjaxMethod]
        public string SendEmailActivationInstructions(Guid userID, string email)
        {
            if (userID == Guid.Empty) throw new ArgumentNullException("userID");

            email = (email ?? "").Trim();
            if (String.IsNullOrEmpty(email)) throw new ArgumentNullException(Resource.ErrorEmailEmpty);
            if (!email.TestEmailRegex()) throw new InvalidEmailException(Resource.ErrorNotCorrectEmail);

            try
            {
                var viewer = CoreContext.UserManager.GetUsers(SecurityContext.CurrentAccount.ID);
                var user = CoreContext.UserManager.GetUsers(userID);

                if (user == null) throw new UserNotFoundException(Resource.ErrorUserNotFound);

                if (viewer == null) throw new AccessDeniedException(Resource.ErrorAccessDenied);

                if (viewer.IsAdmin() || viewer.ID == user.ID)
                {
                    var existentUser = CoreContext.UserManager.GetUserByEmail(email);
                    if (existentUser.ID != ASC.Core.Users.Constants.LostUser.ID && existentUser.ID != userID)
                        throw new InputException(CustomNamingPeople.Substitute<Resource>("ErrorEmailAlreadyExists"));

                    user.Email = email;
                    if (user.ActivationStatus == EmployeeActivationStatus.Activated)
                    {
                        user.ActivationStatus = EmployeeActivationStatus.NotActivated;
                    }
                    if (user.ActivationStatus == (EmployeeActivationStatus.AutoGenerated | EmployeeActivationStatus.Activated))
                    {
                        user.ActivationStatus = EmployeeActivationStatus.AutoGenerated;
                    }
                    CoreContext.UserManager.SaveUserInfo(user);
                }
                else
                {
                    email = user.Email;
                }

                if (user.ActivationStatus == EmployeeActivationStatus.Pending && !user.IsLDAP())
                {
                    if (user.IsVisitor())
                    {
                        StudioNotifyService.Instance.GuestInfoActivation(user);
                    }
                    else
                    {
                        StudioNotifyService.Instance.UserInfoActivation(user);
                    }
                }
                else
                {
                    StudioNotifyService.Instance.SendEmailActivationInstructions(user, email);
                }

                MessageService.Send(HttpContext.Current.Request, MessageAction.UserSentActivationInstructions, user.DisplayUserName(false));

                return String.Format(Resource.MessageEmailActivationInstuctionsSentOnEmail, "<b>" + email + "</b>");
            }
            catch (UserNotFoundException)
            {
                throw;
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (InputException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new Exception(Resource.UnknownError);
            }
        }

        /// <summary>
        /// Sends the email change instructions to the specified email
        /// </summary>
        /// <param name="userID">The ID of the user who is changing the email</param>
        /// <param name="email">Email</param>
        [AjaxMethod]
        public string SendEmailChangeInstructions(Guid userID, string email)
        {
            if (userID == Guid.Empty) throw new ArgumentNullException("userID");

            email = (email ?? "").Trim();
            if (String.IsNullOrEmpty(email)) throw new Exception(Resource.ErrorEmailEmpty);
            if (!email.TestEmailRegex()) throw new Exception(Resource.ErrorNotCorrectEmail);

            try
            {
                var viewer = CoreContext.UserManager.GetUsers(SecurityContext.CurrentAccount.ID);
                var user = CoreContext.UserManager.GetUsers(userID);

                if (user == null)
                    throw new UserNotFoundException(Resource.ErrorUserNotFound);

                if (viewer == null || (user.IsOwner() && viewer.ID != user.ID))
                    throw new AccessDeniedException(Resource.ErrorAccessDenied);

                var existentUser = CoreContext.UserManager.GetUserByEmail(email);
                if (existentUser.ID != ASC.Core.Users.Constants.LostUser.ID)
                    throw new InputException(CustomNamingPeople.Substitute<Resource>("ErrorEmailAlreadyExists"));

                if (!viewer.IsAdmin())
                {
                    StudioNotifyService.Instance.SendEmailChangeInstructions(user, email);
                }
                else
                {
                    if (email == user.Email)
                        throw new InputException(Resource.ErrorEmailsAreTheSame);

                    user.Email = email;
                    if (user.ActivationStatus.HasFlag(EmployeeActivationStatus.AutoGenerated))
                    {
                        user.ActivationStatus = EmployeeActivationStatus.AutoGenerated;
                    }
                    else
                    {
                        user.ActivationStatus = EmployeeActivationStatus.NotActivated;
                    }

                    CoreContext.UserManager.SaveUserInfo(user);
                    StudioNotifyService.Instance.SendEmailActivationInstructions(user, email);
                }

                MessageService.Send(HttpContext.Current.Request, MessageAction.UserSentEmailChangeInstructions, user.DisplayUserName(false));

                return String.Format(Resource.MessageEmailChangeInstuctionsSentOnEmail, "<b>" + email + "</b>");
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (UserNotFoundException)
            {
                throw;
            }
            catch (InputException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new Exception(Resource.UnknownError);
            }
        }
    }
}