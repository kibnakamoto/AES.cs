using System;
using System.Text;

namespace AES
{
    // operations of aes256
    public class OPS_AES256
    {
        // s-box in 1 dimentional constant array.
        private static readonly byte[] Sbox = new byte[256] {
            0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01,
            0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76, 0xca, 0x82, 0xc9, 0x7d,
            0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4,
            0x72, 0xc0, 0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc,
            0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15, 0x04, 0xc7,
            0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2,
            0xeb, 0x27, 0xb2, 0x75, 0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e,
            0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84,
            0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb,
            0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf, 0xd0, 0xef, 0xaa, 0xfb,
            0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c,
            0x9f, 0xa8, 0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5,
            0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2, 0xcd, 0x0c,
            0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d,
            0x64, 0x5d, 0x19, 0x73, 0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a,
            0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb,
            0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3,
            0xac, 0x62, 0x91, 0x95, 0xe4, 0x79, 0xe7, 0xc8, 0x37, 0x6d,
            0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a,
            0xae, 0x08, 0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6,
            0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a, 0x70, 0x3e,
            0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9,
            0x86, 0xc1, 0x1d, 0x9e, 0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9,
            0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf,
            0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99,
            0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16};
        
        // invert s-box in 1 dimentional constant array
        private static readonly byte[] InvSBox = new byte[256] {
            0x52, 0x09, 0x6a, 0xd5, 0x30, 0x36, 0xa5, 0x38, 0xbf, 0x40, 
            0xa3, 0x9e, 0x81, 0xf3, 0xd7, 0xfb, 0x7c, 0xe3, 0x39, 0x82,
            0x9b, 0x2f, 0xff, 0x87, 0x34, 0x8e, 0x43, 0x44, 0xc4, 0xde, 
            0xe9, 0xcb, 0x54, 0x7b, 0x94, 0x32, 0xa6, 0xc2, 0x23, 0x3d,
            0xee, 0x4c, 0x95, 0x0b, 0x42, 0xfa, 0xc3, 0x4e, 0x08, 0x2e,
            0xa1, 0x66, 0x28, 0xd9, 0x24, 0xb2, 0x76, 0x5b, 0xa2, 0x49,
            0x6d, 0x8b, 0xd1, 0x25, 0x72, 0xf8, 0xf6, 0x64, 0x86, 0x68,
            0x98, 0x16, 0xd4, 0xa4, 0x5c, 0xcc, 0x5d, 0x65, 0xb6, 0x92,
            0x6c, 0x70, 0x48, 0x50, 0xfd, 0xed, 0xb9, 0xda, 0x5e, 0x15,
            0x46, 0x57, 0xa7, 0x8d, 0x9d, 0x84, 0x90, 0xd8, 0xab, 0x00,
            0x8c, 0xbc, 0xd3, 0x0a, 0xf7, 0xe4, 0x58, 0x05, 0xb8, 0xb3,
            0x45, 0x06, 0xd0, 0x2c, 0x1e, 0x8f, 0xca, 0x3f, 0x0f, 0x02,
            0xc1, 0xaf, 0xbd, 0x03, 0x01, 0x13, 0x8a, 0x6b, 0x3a, 0x91,
            0x11, 0x41, 0x4f, 0x67, 0xdc, 0xea, 0x97, 0xf2, 0xcf, 0xce,
            0xf0, 0xb4, 0xe6, 0x73, 0x96, 0xac, 0x74, 0x22, 0xe7, 0xad,
            0x35, 0x85, 0xe2, 0xf9, 0x37, 0xe8, 0x1c, 0x75, 0xdf, 0x6e,
            0x47, 0xf1, 0x1a, 0x71, 0x1d, 0x29, 0xc5, 0x89, 0x6f, 0xb7,
            0x62, 0x0e, 0xaa, 0x18, 0xbe, 0x1b, 0xfc, 0x56, 0x3e, 0x4b,
            0xc6, 0xd2, 0x79, 0x20, 0x9a, 0xdb, 0xc0, 0xfe, 0x78, 0xcd,
            0x5a, 0xf4, 0x1f, 0xdd, 0xa8, 0x33, 0x88, 0x07, 0xc7, 0x31,
            0xb1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xec, 0x5f, 0x60, 0x51,
            0x7f, 0xa9, 0x19, 0xb5, 0x4a, 0x0d, 0x2d, 0xe5, 0x7a, 0x9f,
            0x93, 0xc9, 0x9c, 0xef, 0xa0, 0xe0, 0x3b, 0x4d, 0xae, 0x2a,
            0xf5, 0xb0, 0xc8, 0xeb, 0xbb, 0x3c, 0x83, 0x53, 0x99, 0x61,
            0x17, 0x2b, 0x04, 0x7e, 0xba, 0x77, 0xd6, 0x26, 0xe1, 0x69,
            0x14, 0x63, 0x55, 0x21, 0x0c, 0x7d};
        
        // Galois Field Multipication 2^8
        public byte GF8(byte x, byte y)
        {
            // implemented with bitmasking
            byte p=0;
            for (int c=0;c<8;c++) {
                p ^= (byte)(-(y&1)&x);
                x = (byte)((x<<1) ^ (0x11b & -((x>>7)&1)));
                y >>= 1;
            }
            return p;
        }
        
        public byte SubWord(byte x)
        {
            
            return 0;
        }
        
        public byte MixColumns(byte x)
        {
            return 0;
        }
        
        public byte SubBytes(byte b)
        {
            for(int i=0;i<8;i++) {
                
            }
            return 0;
        }
        
        public byte ShiftRows(byte x)
        {
            return 0;
        }

        public byte AddRoundKey(byte[] state)
        {
            for(int c=0;c<4;c++) {
                
            }
            // [w round Nb+c [ ' , ' , ' , ' ]
            //  [ , ]
            
            return 0;
        }
        
        public byte InvShiftRows(byte x)
        {
            return 0;
        }
        
        public byte InvSubBytes(byte x)
        {
            return 0;
        }
        
        public byte InvMixColumns(byte x)
        {
            return 0;
        }

    }
    
    public class AES256
    {
        public string Encrypt(string UserIn)
        {
            OPS_AES256 Operation = new OPS_AES256();
            /* put things from sec 2.2 from FIPS 197 AES
            (byte in[4*Nb], byte out[4*Nb], word w[Nb*(Nr+1)])
            
            begin
            
            byte state[4,Nb]
            
            state = in
            
            AddRoundKey(state, w[Nr*Nb, (Nr+1)*Nb-1]) // See Sec. 5.1.4
            
            for round = Nr-1 step -1 downto 1
            
            InvShiftRows(state) // See Sec. 5.3.1
            
            InvSubBytes(state) // See Sec. 5.3.2
            
            AddRoundKey(state, w[round*Nb, (round+1)*Nb-1])
            
            InvMixColumns(state) // See Sec. 5.3.3
            
            end for
            
            InvShiftRows(state)
            
            InvSubBytes(state)
            
            AddRoundKey(state, w[0, Nb-1])
            
            out = state
            
            end
             
            */
            // initialize arrays and the state matrix.
            byte[] Input = new byte[16];
            Input = System.Text.Encoding.ASCII.GetBytes(UserIn);
            byte[,] state = new byte[4,4];
            byte[] output = new byte[16];

            // put 1 dimentional array values to a 2 dimentional matrix.
            for(int r=0;r<4;r++) {
                for(int c=0;c<4;c++)
                    state[c,r] = Input[c+4*r];
            }
            
            // copy state array to output
            for(int r=0;r<4;r++) {
                for(int c=0;c<4;c++)
                    output[c+4*r] = state[r,c];
            }

            // for testing values
            for(int r=0;r<4;r++) {
                for(int c=0;c<4;c++) {
                    char ch = Convert.ToChar(state[c,r]);
                    string d = ch.ToString();
                    // Console.Write($"\nstate: {d}\t\t\tindexes:\tc:{c}\tr:{r}");
                }
            }
            
            if(Input.Length != 16) {
                // || W.Length != 480  || output.Length != 128
                // add when W and output is defined.
                throw new ArgumentException("length doesn't match");
            }
            return null;
        }
        public string Decrypt(string UserIn)
        {
            return null;
        }
    }
}
