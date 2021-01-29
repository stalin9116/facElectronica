using es.mityc.firmaJava.libreria.xades;
using es.mityc.javasign.pkstore;
using es.mityc.javasign.pkstore.keystore;
using es.mityc.javasign.xml.refs;
using java.io;
using java.security;
using java.security.cert;
using java.util;
using javax.xml.parsers;
using Microsoft.SqlServer.Server;
using org.bouncycastle.asn1;
using org.w3c.dom;
using sviudes.blogspot.com;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace LayerLogic.ClassLibrary.Complementos
{
    public class Firmar
    {
        public static string encoding = "UTF-8";
        public string tipoFile = "text/xml";
        public string sufijo = "firmado";
        public static string nodoFirma = "comprobante";

        public Firmar()
        {

        }

        public static java.security.cert.X509Certificate loadCertificate(string patchCerticate, string clave, out PrivateKey privatekey, out Provider provider)
        {
            java.security.cert.X509Certificate certificate = null;
            provider = null;
            privatekey = null;

            //Cargar el certificado Digital
            KeyStore ks = KeyStore.getInstance("PKCS12");
            ks.load(new BufferedInputStream(new FileInputStream(patchCerticate)), clave.ToCharArray());
            IPKStoreManager storeManager = new KSStore(ks, new PassStoreKS(clave));
            //Cargar certificados
            List certificates = storeManager.getSignCertificates();
            if (certificates.size() > 0)
            {
                certificate = (java.security.cert.X509Certificate)certificates.get(1);

                //Obtener la clave privada asociada al certificado
                privatekey = storeManager.getPrivateKey(certificate);

                //Obtener el Proveedorde la criptografía
                provider = storeManager.getProvider(certificate);
            }

            return certificate;
        }


        public static void firmar(string Archivo)
        {
            string path = @"C:\Firma\fabricio_fortunato_mero_mosquera.p12";
            PrivateKey privatekey;
            Provider provider;

            java.security.cert.X509Certificate certificate = LayerLogic.ClassLibrary.Complementos.Firmar.loadCertificate(path, "FFmm_1978", out privatekey, out provider);
            if (certificate!=null)
            {
                //Creamos el documento a firmar
                DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();
                dbf.setNamespaceAware(true);
                DocumentBuilder db = dbf.newDocumentBuilder();

                //C#
                var base64 = System.Convert.FromBase64String(Archivo);
                string bytes = System.Text.Encoding.UTF8.GetString(base64);

                ByteArrayInputStream bs = new ByteArrayInputStream(System.Text.Encoding.UTF8.GetBytes(bytes));
                Document documento = dbf.newDocumentBuilder().parse(bs);
                //Creamos datos a firmar 

                DataToSign dataToSign = new DataToSign();
                dataToSign.setXadesFormat(EnumFormatoFirma.XAdES_BES); //XAdES-EPES
                dataToSign.setAddPolicy(false);
                dataToSign.setXMLEncoding(encoding);
                dataToSign.setEnveloped(true);
                dataToSign.addObject(new ObjectToSign(new InternObjectToSign(nodoFirma), "comprobante", null, "text/xml", null));
                dataToSign.setParentSignNode(nodoFirma);
                //dataToSign.setDocument(LoadXML(NombreArchivo));
                dataToSign.setDocument(documento);

                //Firmar
                Object[] res = new FirmaXML().signFile(certificate, dataToSign, privatekey, provider);

                Document doc = (Document)res[0];
                //Transformar a string
                org.w3c.dom.ls.DOMImplementationLS domImplementation = (org.w3c.dom.ls.DOMImplementationLS)doc.getImplementation();
                org.w3c.dom.ls.LSSerializer lsSerializer = domImplementation.createLSSerializer();
                Archivo = lsSerializer.writeToString(doc).Replace("UTF-16", "UTF-8");

                //C#

                var ArchivoFirmado = Encoding.UTF8.GetBytes(Archivo);
                string firmado = Convert.ToBase64String(ArchivoFirmado);

                var base642 = System.Convert.FromBase64String(firmado);
                string bytes2 = System.Text.Encoding.UTF8.GetString(base642);



            }


        }



    }
}
