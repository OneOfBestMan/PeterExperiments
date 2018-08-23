# encoding:utf-8
# python3.0

from data.save import saveData
from data.common import getHtml
import configparser


# ------主函数------
# delete()
if __name__ == '__main__':
    # 获取参数
    config = configparser.ConfigParser()
    config.read("config.ini")

    # 清除数据
    save = saveData(config)
    save.deleteOldData()

 

    # 58同城 高新园区 80-120W 3室 精装修
    tongcheng1 = getHtml('''http://bj.58.com/ershoufang/?PGTID=0d00000c-0000-099e-5f9d-eb7cd9b2d735&ClickID=1&huansuanyue=200_600&bunengdaikuan=0&area=60_100''')
    tongcheng2 = getHtml('''http://bj.58.com/ershoufang/pn2/?huansuanyue=200_600&bunengdaikuan=0&area=60_100&PGTID=0d300000-0000-0b90-1e0b-bf894f74b13a&ClickID=1''')
    tongcheng3 = getHtml('''http://bj.58.com/ershoufang/pn3/?huansuanyue=200_600&bunengdaikuan=0&area=60_100&PGTID=0d300000-0000-08f9-ba56-6673c850e2b8&ClickID=1''')

    ##tongcheng1 = getHtml('''https://sh.58.com/hezu/35024931760976x.shtml?PGTID=0d3090a7-0057-e333-6eb5-681130d5f519&ClickID=2&adtype=3''')
    #tongcheng2 = getHtml('''https://sh.58.com/hongkou/hezu/?PGTID=0d40000a-005c-c312-af1e-9e6995d81c66&ClickID=6''')
    #tongcheng3 = getHtml('''https://sh.58.com/hongkou/hezu/0/?PGTID=0d30000a-0057-e4ce-f81e-fded112d88ea&ClickID=2''')

    # print(str(tongcheng1.encode('GB18030')))
    tongcheng_htmls = [tongcheng1, tongcheng2, tongcheng3]
    for tongcheng_html in tongcheng_htmls:
        save.tongcheng_save(tongcheng_html)

    # 安居客 （例：北京 200-600万 60-100平 按最新排序） 根据自己需求添加链接
    #anjuke1 = getHtml('''https://beijing.anjuke.com/sale/o5/?from_area=60&to_area=100&from_price=200&to_price=600''')
    #anjuke2 = getHtml('''https://beijing.anjuke.com/sale/o5-p2/?from_area=60&to_area=100&from_price=200&to_price=600#filtersort''')
    #anjuke3 = getHtml('''https://beijing.anjuke.com/sale/o5-p3/?from_area=60&to_area=100&from_price=200&to_price=600#filtersort''')
    #anjuke_htmls = [anjuke1, anjuke2, anjuke3]
    #for anjuke_html in anjuke_htmls:
    #    save.anjuke_save(anjuke_html)

 

    print("OVER!!!")
