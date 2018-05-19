using System;
using System.Linq;
using System.Text;
using XenAPI;


namespace GetVmRecords
{
    static class GetVariousRecords
    {
        private static string newline = Environment.NewLine;

        public static string Run(Session session)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Printing various records" + newline);
            sb.Append(PrintHostRecords(session) + newline);
            sb.Append(PrintStorageRepositories(session) + newline);
            sb.Append(PrintVmRecords(session) + newline);
            sb.Append(PrintPhysicalNetworkInterfaces(session) + newline);
            sb.Append(newline);
            return sb.ToString();
        }

        private static string PrintHostRecords(Session session)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Hosts" + newline);
            var hostRecords = Host.get_all_records(session);

            foreach (var hostRec in hostRecords)
            {
                var host = hostRec.Value;
                sb.Append(String.Format("Name: {0}", host.name_label) + newline);
                sb.Append(String.Format("Hostname: {0}", host.hostname) + newline);
                sb.Append(String.Format("Description: {0}", host.name_description) + newline);
                sb.Append(newline);
            }

            return sb.ToString();
        }

        private static string PrintStorageRepositories(Session session)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Storage Repositories" + newline);
            var srRecords = SR.get_all_records(session);

           foreach (var srRec in srRecords)
            {
                var sr = srRec.Value;
                sb.Append(String.Format("Name: {0}", sr.name_label) + newline);
                sb.Append(String.Format("Description: {0}", sr.name_description) + newline);
                sb.Append(String.Format("Usage: {0:0.0}GB / {1:0.0}GB", sr.physical_utilisation / 1e9, sr.physical_size / 1e9) + newline);
                sb.Append(newline);
            }

            return sb.ToString();
        }

        private static string PrintVmRecords(Session session)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Virtual Machines" + newline);

            var vmRecords = VM.get_all_records(session);
            foreach (var vmRec in vmRecords)
            {
                var vm = vmRec.Value;
                sb.Append(String.Format(vm.is_a_template ? "VM name: {0}" : "Template name {0}", vm.name_label) + newline);
                sb.Append(String.Format("Power state: {0}", vm.power_state) + newline);
                string ops = string.Join(",", vm.allowed_operations.Select(op => op.ToString()));
                sb.Append(String.Format("Allowed operations: {0}", ops) + newline);
                sb.Append(String.Format("vCPUs: {0}", vm.VCPUs_at_startup) + newline);
                sb.Append(newline);
            }

            return sb.ToString();
        }

        private static string PrintPhysicalNetworkInterfaces(Session session)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Physical network interfaces" + newline);
            var pifRecords = PIF.get_all_records(session);

            foreach (var pifRec in pifRecords)
            {
                var pif = pifRec.Value;

                Host host = Host.get_record(session, pif.host);
                sb.Append(String.Format("Host: {0}", host.name_label) + newline);
                sb.Append(String.Format("IP: {0}", pif.IP) + newline);
                sb.Append(String.Format("MAC address: {0}", pif.MAC) + newline);
                sb.Append(newline);
            }

            return sb.ToString();
        }

    }
}
