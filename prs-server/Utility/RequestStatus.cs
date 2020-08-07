using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrsServer.Utility {

    public static class RequestStatus {
        public static string New = "NEW";
        public static string Edit = "EDIT";
        public static string Review = "REVIEW";
        public static string Approved = "APPROVED";
        public static string Rejected = "REJECTED";
    }
}
