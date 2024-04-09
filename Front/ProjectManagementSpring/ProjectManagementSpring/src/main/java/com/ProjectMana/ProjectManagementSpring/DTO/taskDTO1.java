package com.ProjectMana.ProjectManagementSpring.DTO;

import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
public class taskDTO1 {
    public Integer no ;
    public String title ;
    public String description ;
    public String projectName;
    public String userName;
    public String end ;
    public Integer status ;

    public taskDTO1(Integer no, String title, String description, String projectName, String userName, String end, Integer status) {
        this.no = no;
        this.title = title;
        this.description = description;
        this.projectName = projectName;
        this.userName = userName;
        this.end = end;
        this.status = status;
    }
}
