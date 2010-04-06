unsigned char GPGGA[100];
char GPGSA[100];
char GPGSV[100];
char GPRMC[100];
 
void GPS (void)
        {
        if(rx_buffer1[rx_wr_index1 - 5] == '$')
                {
                if(rx_buffer1[rx_wr_index1 - 4] == 'G')
                        {
                        if(rx_buffer1[rx_wr_index1 - 3] == 'P')
                                {
                                switch(rx_buffer1[rx_wr_index1 - 2])
                                        { 
                                        case 'G':
                                                switch(rx_buffer1[rx_wr_index1 - 1])
                                                        {
                                                        case 'G':
                                                                if(rx_buffer1[rx_wr_index1] == 'A')
                                                                        {
                                                                        GPGGA[6]="$GPGGA";
                                                                        
                                                                        }
                                                                break;
                                                        case 'S':
                                                                switch(rx_buffer1[rx_wr_index1])
                                                                        {
                                                                        case 'A':
                                                                                //$GPGSA
                                                                                break;
                                                                        case 'V':
                                                                                //$GPGSV
                                                                                break;
                                                                        default:
                                                                                break;
                                                                        
                                                                        }
                                                        
                                                                break;
                                                        default:
                                                                break;
                                                        }
                                                break;
                                        case 'R':
                                                if(rx_buffer1[rx_wr_index1 - 1] == 'M')
                                                        {
                                                        if(rx_buffer1[rx_wr_index1] == 'C')
                                                                {
                                                                //$GPRMC
                                                                }
                                                        }
                                                break;
                                        default:
                                                break;
                                        };
                                }
                        }
                }                
        }